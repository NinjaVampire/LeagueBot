﻿using LeagueBot.Api;
using LeagueBot.IO;
using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Scripting.Patterns
{
    public class Script
    {
        public string Filename
        {
            get;
            set;
        }
        private string Description
        {
            get;
            set;
        }
        private Lua Lua
        {
            get;
            set;
        }
        public Script(string fileName, Lua lua)
        {
            this.Filename = fileName;

            var winApi = new WinApi(lua);

            lua["win"] = winApi;
            lua["img"] = new ImgApi(winApi);
            lua["game"] = new GameApi(winApi);

            this.Description = lua.GetString("Description");
            this.Lua = lua;
        }

        public void Execute(params object[] param)
        {
            Logger.Write("Running " + Filename);
            LuaFunction functionMain = Lua.GetFunction("Execute");
            functionMain.Call(param);
        }
        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(Filename) + " \"" + Description + "\"";
        }
    }
}
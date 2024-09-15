using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VillaAG.Main.Infrastructure {
    public class FileActionResults : ActionResult {

        public string _contentType { get; private set; }
        public string _fileName { get; private set; }
        public string _filePath { get; private set; }


        public FileActionResults(string filename, string filepath, string contenttype)
        {
            _fileName = filename;
            _filePath = filepath;
            _contentType = contenttype;
        }

        public override void ExecuteResult(ControllerContext context) {
            var filepathname = _filePath + _fileName;
            var bytes = File.ReadAllBytes(context.HttpContext.Server.MapPath(filepathname));
            context.HttpContext.Response.ContentType = _contentType;
            context.HttpContext.Response.BinaryWrite(bytes);
        }
    }
}
﻿namespace WebApp.Adapter.Services
{
    public interface IImageProcess
    {
        void AddWatermark(string text, string filename, Stream imageStream);
    }
}

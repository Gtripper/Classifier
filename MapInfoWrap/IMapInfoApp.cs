using System;
using MapInfo;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public interface IMapInfoApp
    {
        DMapInfo CreateInstance();
    }
    /// <summary>
    /// Создает новый экземпляр MapInfo
    /// </summary>
    public class MapInfoNewApp : IMapInfoApp
    {
        public DMapInfo CreateInstance()
        {
            Type mapinfotype = Type.GetTypeFromProgID("Mapinfo.Application");
            return (DMapInfo)Activator.CreateInstance(mapinfotype);
        }
    }
    /// <summary>
    /// Создает представление текущего просесса MapInfo
    /// </summary>
    /// <remark>
    /// В случае открытия нескольких просессов мапинфо, 
    /// работа будет идти с первым открытым экземпляром
    /// </remark>
    public class MapinfoCurrentApp : IMapInfoApp
    {
        public DMapInfo CreateInstance()
        {
            Type mapinfotype = Type.GetTypeFromProgID("Mapinfo.Application");
            return (MapInfoApplication)Marshal.GetActiveObject("Mapinfo.Application");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiAngularJS.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {

        public class Device
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private static List<Device> Devices;
        private void loadDevices(bool reload)
        {
            if (Devices == null || reload)
            {
                Devices = new List<Device>();
            }
            if (Devices.Count() < 1)
            {
                Devices.Add(new Controllers.ValuesController.Device { Id = 1, Name = "Nexus One" });
                Devices.Add(new Device { Id = 2, Name = "Nexus S" });
                Devices.Add(new Device { Id = 3, Name = "Nexus 7" });
                Devices.Add(new Device { Id = 4, Name = "Nexus 4" });
                Devices.Add(new Device { Id = 5, Name = "Nexus 5" });
                Devices.Add(new Device { Id = 6, Name = "Nexus 5X" });
            }
        }

        private List<Device> getAndroidDevices()
        {
            loadDevices(false);
            return Devices;
        }


        [HttpGet]
        //[Route("{api/devices")]
        public List<Device> Get()
        {
            return this.getAndroidDevices();
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Device))]
        public Device GetDevice(int id)
        {
            var device = getAndroidDevices().FirstOrDefault(d => d.Id == id);
            return device;
        }


        // POST api/values
        public List<Device> Post([FromBody]Device value)
        {
            int maxId = Devices.Max(d => d.Id);
            value.Id = maxId + 1;
            Devices.Add(value);
            return this.getAndroidDevices();
        }


        [Route("api/Values/Reload")]
        [ActionName("Reload")]
        [HttpGet]
        public List<Device> Reload()
        {
            loadDevices(true);
            return this.getAndroidDevices();
        }

        // PUT api/values/5
        public List<Device> Put(int id, [FromBody]Device value)
        {
            Device device = getAndroidDevices().FirstOrDefault(d => d.Id == id);
            int maxId = Devices.Max(d => d.Id);
            if (device == null)
            {
                value.Id = maxId + 1;
                Devices.Add(value);
            } else
            {
                device.Name = value.Name;
            }
            return this.getAndroidDevices();
        }

        // DELETE api/values/5
        public List<Device> Delete(int id)
        {
            Devices.Remove(getAndroidDevices().First(d => d.Id == id));
            return this.getAndroidDevices();
        }
    }
}

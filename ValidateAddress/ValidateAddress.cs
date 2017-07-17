using System;
using System.Net;
using System.Xml.Linq;

namespace ValidateAddress
{
    // https://developers.google.com/maps/documentation/geocoding/start
    public static class addressApi
    {
        public static string FormattedAddress { get; private set; }
        public static string Lattitude { get; private set; }
        public static string Longnitude { get; private set; }
        public static string Error { get; private set; }
        public enum Result { Failed = 0, Succeeded = 1 }
        public static Result CheckAddress(string houseNum, string street, string city, string state, string zip, string format)
        {
            Result blnFailed = Result.Succeeded;
            try
            {
                var address = $"{houseNum}, {street}, {city}, {state}, {zip}";
                string url = $"http://";
                string urlEnd = $"maps.googleapis.com/maps/api/geocode/{format}?address={address}&sensor=false";
                url = url + urlEnd;

                var requestUri = string.Format(url, Uri.EscapeDataString(address));

                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());

                var result = xdoc.Element("GeocodeResponse").Element("result");

                FormattedAddress = result.Element("formatted_address").Value;                 
                
                var locationElement = result.Element("geometry").Element("location");
                Lattitude = locationElement.Element("lat").Value;
                Longnitude = locationElement.Element("lng").Value;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                blnFailed = Result.Failed;
            }

            return blnFailed;
        }
    }

    // https://stackoverflow.com/questions/9256016/calling-vb6-dlls-from-c-sharp


}



/*
<result>
  <type>street_address</type>
  <formatted_address>2505 Nolensville Pike, Nashville, TN 37211, USA</formatted_address>
  <address_component>
    <long_name>2505</long_name>
    <short_name>2505</short_name>
    <type>street_number</type>
  </address_component>
  <address_component>
    <long_name>Nolensville Pike</long_name>
    <short_name>Nolensville Pike</short_name>
    <type>route</type>
  </address_component>
  <address_component>
    <long_name>South Nashville</long_name>
    <short_name>South Nashville</short_name>
    <type>neighborhood</type>
    <type>political</type>
  </address_component>
  <address_component>
    <long_name>Nashville</long_name>
    <short_name>Nashville</short_name>
    <type>locality</type>
    <type>political</type>
  </address_component>
  <address_component>
    <long_name>Davidson County</long_name>
    <short_name>Davidson County</short_name>
    <type>administrative_area_level_2</type>
    <type>political</type>
  </address_component>
  <address_component>
    <long_name>Tennessee</long_name>
    <short_name>TN</short_name>
    <type>administrative_area_level_1</type>
    <type>political</type>
  </address_component>
  <address_component>
    <long_name>United States</long_name>
    <short_name>US</short_name>
    <type>country</type>
    <type>political</type>
  </address_component>
  <address_component>
    <long_name>37211</long_name>
    <short_name>37211</short_name>
    <type>postal_code</type>
  </address_component>
  <address_component>
    <long_name>2210</long_name>
    <short_name>2210</short_name>
    <type>postal_code_suffix</type>
  </address_component>
  <geometry>
    <location>
      <lat>36.1188295</lat>
      <lng>-86.7493588</lng>
    </location>
    <location_type>ROOFTOP</location_type>
    <viewport>
      <southwest>
        <lat>36.1174805</lat>
        <lng>-86.7507078</lng>
      </southwest>
      <northeast>
        <lat>36.1201785</lat>
        <lng>-86.7480098</lng>
      </northeast>
    </viewport>
  </geometry>
  <partial_match>true</partial_match>
  <place_id>ChIJS1bnVPBlZIgRozKhntlq0rU</place_id>
</result>
*/

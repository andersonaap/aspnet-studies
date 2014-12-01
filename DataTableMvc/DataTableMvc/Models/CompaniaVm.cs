using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DataTableMvc.Models
{
    public class CompaniaVm : JQueryDataTableParamModel
    {
        public string id { get; set; }
        public string compania { get; set; }
        public string pais { get; set; }
        public string preco { get; set; }

    }


    public static class CompaniaRepository
    {
        public static CompaniaVm[] Obter() 
        {
            var doc = XDocument.Parse(xml);

            var companias =
                doc.Root.Elements("produto")
                    .Select(x =>
                        new CompaniaVm
                        {
                            id = x.Element("id").Value,
                            compania = x.Element("compania").Value,
                            pais = x.Element("pais").Value,
                            preco = x.Element("preco").Value,
                        })
                   .ToArray();

            return companias;
        }



        const string xml = @"
<produtos>
	<produto>
		<id>1</id>
		<compania>Vel Est Company</compania>
		<pais>Dominican Republic</pais>
		<preco>$22.56</preco>
	</produto>
	<produto>
		<id>2</id>
		<compania>Augue Eu PC</compania>
		<pais>Burkina Faso</pais>
		<preco>$35.69</preco>
	</produto>
	<produto>
		<id>3</id>
		<compania>Donec At Foundation</compania>
		<pais>Slovakia</pais>
		<preco>$26.86</preco>
	</produto>
	<produto>
		<id>4</id>
		<compania>Nibh Enim Institute</compania>
		<pais>Iceland</pais>
		<preco>$55.63</preco>
	</produto>
	<produto>
		<id>5</id>
		<compania>Lacinia At Iaculis Industries</compania>
		<pais>Belize</pais>
		<preco>$44.01</preco>
	</produto>
	<produto>
		<id>6</id>
		<compania>Enim Nisl Inc.</compania>
		<pais>Chad</pais>
		<preco>$30.81</preco>
	</produto>
	<produto>
		<id>7</id>
		<compania>Nec Orci Donec Incorporated</compania>
		<pais>Comoros</pais>
		<preco>$53.44</preco>
	</produto>
	<produto>
		<id>8</id>
		<compania>Tortor Nibh PC</compania>
		<pais>Sint Maarten</pais>
		<preco>$60.17</preco>
	</produto>
	<produto>
		<id>9</id>
		<compania>Id Associates</compania>
		<pais>Peru</pais>
		<preco>$40.59</preco>
	</produto>
	<produto>
		<id>10</id>
		<compania>Nam LLP</compania>
		<pais>British Indian Ocean Territory</pais>
		<preco>$48.28</preco>
	</produto>
	<produto>
		<id>11</id>
		<compania>Vel Incorporated</compania>
		<pais>Sweden</pais>
		<preco>$24.78</preco>
	</produto>
	<produto>
		<id>12</id>
		<compania>Laoreet Inc.</compania>
		<pais>Zimbabwe</pais>
		<preco>$28.57</preco>
	</produto>
	<produto>
		<id>13</id>
		<compania>Fusce Corporation</compania>
		<pais>Palestine, State of</pais>
		<preco>$33.90</preco>
	</produto>
	<produto>
		<id>14</id>
		<compania>Elit Erat PC</compania>
		<pais>Guinea-Bissau</pais>
		<preco>$65.38</preco>
	</produto>
	<produto>
		<id>15</id>
		<compania>Morbi Limited</compania>
		<pais>Nauru</pais>
		<preco>$39.20</preco>
	</produto>
	<produto>
		<id>16</id>
		<compania>Ridiculus Mus Consulting</compania>
		<pais>Russian Federation</pais>
		<preco>$44.59</preco>
	</produto>
	<produto>
		<id>17</id>
		<compania>Risus LLC</compania>
		<pais>Sweden</pais>
		<preco>$42.62</preco>
	</produto>
	<produto>
		<id>18</id>
		<compania>Arcu Ac Orci PC</compania>
		<pais>Nauru</pais>
		<preco>$37.76</preco>
	</produto>
	<produto>
		<id>19</id>
		<compania>Lectus Pede Corporation</compania>
		<pais>Nepal</pais>
		<preco>$49.04</preco>
	</produto>
	<produto>
		<id>20</id>
		<compania>Litora Torquent Per Institute</compania>
		<pais>Azerbaijan</pais>
		<preco>$38.48</preco>
	</produto>
	<produto>
		<id>21</id>
		<compania>Cras Eu Tellus Corporation</compania>
		<pais>United Kingdom (Great Britain)</pais>
		<preco>$53.63</preco>
	</produto>
	<produto>
		<id>22</id>
		<compania>In Felis Corporation</compania>
		<pais>Lebanon</pais>
		<preco>$52.20</preco>
	</produto>
	<produto>
		<id>23</id>
		<compania>Nunc Ac Inc.</compania>
		<pais>Sri Lanka</pais>
		<preco>$67.55</preco>
	</produto>
	<produto>
		<id>24</id>
		<compania>Cursus Purus Industries</compania>
		<pais>Morocco</pais>
		<preco>$31.33</preco>
	</produto>
	<produto>
		<id>25</id>
		<compania>Integer Associates</compania>
		<pais>Iran</pais>
		<preco>$60.00</preco>
	</produto>
	<produto>
		<id>26</id>
		<compania>Sociosqu Ad Corp.</compania>
		<pais>Christmas Island</pais>
		<preco>$40.98</preco>
	</produto>
	<produto>
		<id>27</id>
		<compania>Aliquet Libero PC</compania>
		<pais>Qatar</pais>
		<preco>$32.86</preco>
	</produto>
	<produto>
		<id>28</id>
		<compania>Ipsum Cursus Vestibulum Corporation</compania>
		<pais>Chad</pais>
		<preco>$34.93</preco>
	</produto>
	<produto>
		<id>29</id>
		<compania>At Auctor Ullamcorper Incorporated</compania>
		<pais>Bulgaria</pais>
		<preco>$23.29</preco>
	</produto>
</produtos>
";
    }

    

}
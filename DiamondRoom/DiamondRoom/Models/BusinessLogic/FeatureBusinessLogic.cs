using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class FeatureBusinessLogic
    {
        private DiamondRoomEntities6 context = new DiamondRoomEntities6();
        public ObservableCollection<Feature> Features { get; set; }

        public FeatureBusinessLogic()
        {
            Features = new ObservableCollection<Feature>();
        }
        public ObservableCollection<Feature> GetAllFeatures()
        {
            List<Feature> features = context.Features.ToList();
            ObservableCollection<Feature> result = new ObservableCollection<Feature>();
            foreach (Feature con in features)
            {
                result.Add(con);
            }
            return result;
        }

        public void AddFeature(object obj)
        {
            Feature feature = obj as Feature;
            if (feature != null)
            {
                if (string.IsNullOrEmpty(feature.description))
                {
                    MessageBox.Show("Completeaza campul (description)");
                }
                else
                {
                    //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                    //fara a utiliza procedura stocata AddPerson
                    context.Features.Add(new Feature() { description = feature.description });
                    context.SaveChanges();
                    feature.id = context.Contacts.Max(item => item.id);
                    Features.Add(feature);
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            Feature feature = obj as Feature;
            if (feature == null)
            {
                MessageBox.Show("Selecteaza un camp!");
            }
            else
            {
                context.Features.Remove(feature);
                context.SaveChanges();
                Features.Remove(feature);
            }
        }

        public void ModifyFeature(object obj)
        {
            Feature con = obj as Feature;
            if (con == null)
            {
                MessageBox.Show("Selecteaza o optiune!");
            }
            else if (string.IsNullOrEmpty(con.description))
            {
                MessageBox.Show("Precizeaza noile campuri!");
            }
            else
            {
                context.ModifyFeature(con.id, con.description);
                context.SaveChanges();
            }
        }
    }
}

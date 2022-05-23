using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class ExtraFeatureBusinessLogic
    {
        private DiamondRoomEntities6 context = new DiamondRoomEntities6();
        public ObservableCollection<Extra_features> ExtraFeatures { get; set; }

        public ExtraFeatureBusinessLogic()
        {
            ExtraFeatures = new ObservableCollection<Extra_features>();
        }

        public ObservableCollection<Extra_features> GetAllExtraFeatures()
        {
            List<Extra_features> features = context.Extra_features.ToList();
            ObservableCollection<Extra_features> result = new ObservableCollection<Extra_features>();
            foreach (Extra_features con in features)
            {
                result.Add(con);
            }
            return result;
        }

        public void AddExtraFeature(object obj)
        {
            Extra_features feature = obj as Extra_features;
            if (feature != null)
            {
                if (string.IsNullOrEmpty(feature.service) && feature.price != null )
                {
                    MessageBox.Show("Completeaza campurile corespunzator!");
                }
                else
                {
                    //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                    //fara a utiliza procedura stocata AddPerson
                    context.Extra_features.Add(new Extra_features() { price = feature.price, deleted = feature.deleted, service = feature.service}); 
                    context.SaveChanges();
                    feature.id = context.Extra_features.Max(item => item.id);
                    ExtraFeatures.Add(feature);
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            Extra_features feature = obj as Extra_features;
            if (feature == null)
            {
                MessageBox.Show("Selecteaza o extra-optiune!");
            }
            else
            {
                context.Extra_features.Remove(feature);
                context.SaveChanges();
                ExtraFeatures.Remove(feature);
            }
        }

        public void ModifyFeature(object obj)
        {
            Extra_features con = obj as Extra_features;
            if (con == null)
            {
                MessageBox.Show("Selecteaza o extra-optiune!");
            }
            else if (string.IsNullOrEmpty(con.service))
            {
                MessageBox.Show("Precizeaza noile campuri!");
            }
            else
            {
                context.ModifyExtraFeature(con.id, con.price, con.deleted, con.service);
                context.SaveChanges();
            }
        }
    }
}

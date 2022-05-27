using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models.BusinessLogic
{
    public class RoomFeaturesBusinessLogic
    {
        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Rooms_features> RoomFeatures { get; set; }
        public ObservableCollection<Feature> FeaturesForSpecifiedRoom { get; set; }

        public RoomFeaturesBusinessLogic()
        {
            FeaturesForSpecifiedRoom = new ObservableCollection<Feature>();
        }
        public ObservableCollection<Feature> GetFeaturesForSpecifiedRoomID(int roomID)
        {
            List<Rooms_features> roomFeatures = context.Rooms_features.ToList();
            List<Feature> features = context.Features.ToList();
            ObservableCollection<Feature> result = new ObservableCollection<Feature>();

            foreach(Rooms_features roomFeature in roomFeatures)
            {
                if(roomFeature.fk_room == roomID)
                {
                    foreach(Feature feature in features)
                    {
                        if(feature.id == roomFeature.id)
                        {
                            result.Add(feature);
                        }
                    }
                }
            }
            
            return result;
        }

    }
}

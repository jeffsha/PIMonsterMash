using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash
{
    class StatsManager
    {
        public StatsManager(string playerName, string serverName)
        {
            this.playerName = playerName;

            var currentPISystem = PISystem.CreatePISystem(serverName, true);
            server = PIServer.FindPIServer(currentPISystem, serverName);

            server.Connect();
        }

        private string playerName;

        private PIServer server;

        private string scoreTagName {
            get { return $"{playerName}-score"; }
        }

        private string attackRollsTagName {
            get { return $"{playerName}-attack-rolls"; }
        }

        private string damageRollsTagName {
            get { return $"{playerName}-damage-rolls"; }
        }

        public void IntializePlayerStats()
        {
            // PlayerName - Assuming full name/Unique name
            // Try to create if not exist
            var points = PIPoint.FindPIPoints(server, playerName + "*");

            if (points.Count() < 1)
            {
                IDictionary<string, object> intAttributeProperties = new Dictionary<string, object> {
                    { PICommonPointAttributes.PointType, PIPointType.Int32 },
                    { PICommonPointAttributes.Compressing, 0 },
                    { PICommonPointAttributes.Shutdown, 0 }
                };

                IDictionary<string, IDictionary<string, object>> pointsAttributesTable = new Dictionary<string, IDictionary<string, object>>();
                pointsAttributesTable.Add(scoreTagName, intAttributeProperties);
                pointsAttributesTable.Add(attackRollsTagName, intAttributeProperties);
                pointsAttributesTable.Add(damageRollsTagName, intAttributeProperties);

                server.CreatePIPoints(pointsAttributesTable);
            }
        }

        public void UpdateScoreStat(int score)
        {
            var stat = PIPoint.FindPIPoint(server, scoreTagName);
            stat.UpdateValue(new AFValue() { Value = score, PIPoint = stat }, AFUpdateOption.InsertNoCompression);
        }

        public void UpdateAttackRollStat(int attackRoll)
        {
            var stat = PIPoint.FindPIPoint(server, attackRollsTagName);
            stat.UpdateValue(new AFValue() { Value = attackRoll, PIPoint = stat }, AFUpdateOption.InsertNoCompression);
        }

        public void UpdateDamageRollStat(int damageRoll)
        {
            var stat = PIPoint.FindPIPoint(server, damageRollsTagName);
            stat.UpdateValue(new AFValue() { Value = damageRoll, PIPoint = stat }, AFUpdateOption.InsertNoCompression);
        }
    }
}

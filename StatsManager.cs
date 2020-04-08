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
        private PIPoint scoreTag;

        private string attackRollsTagName {
            get { return $"{playerName}-attack-rolls"; }
        }
        private PIPoint attackRollsTag;

        private string damageRollsTagName {
            get { return $"{playerName}-damage-rolls"; }
        }
        private PIPoint damageRollsTag;

        public void IntializePlayerStats()
        {
            var intAttributeProperties = new Dictionary<string, object> {
                    { PICommonPointAttributes.PointType, PIPointType.Int32 },
                    { PICommonPointAttributes.Compressing, 0 },
                    { PICommonPointAttributes.Shutdown, 0 }
                };

            // PlayerName - Assuming full name/Unique name
            // Try to create if not exist

            PIPoint.TryFindPIPoint(server, scoreTagName, out scoreTag);
            if (scoreTag == null)
            {
                scoreTag = server.CreatePIPoint(scoreTagName, intAttributeProperties);
            }

            PIPoint.TryFindPIPoint(server, attackRollsTagName, out attackRollsTag);
            if (attackRollsTag == null)
            {
                attackRollsTag = server.CreatePIPoint(attackRollsTagName, intAttributeProperties);
            }

            PIPoint.TryFindPIPoint(server, damageRollsTagName, out damageRollsTag);
            if (damageRollsTag == null)
            {
                damageRollsTag = server.CreatePIPoint(damageRollsTagName, intAttributeProperties);
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

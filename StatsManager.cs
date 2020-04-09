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
        int monstersKilled = 0;

        public StatsManager(string playerName, string serverName)
        {
            this.playerName = playerName;
            monstersKilled = 0;

            var currentPISystem = PISystem.CreatePISystem(serverName, true);
            server = PIServer.FindPIServer(currentPISystem, serverName);

            server.Connect();
        }

        private string playerName;

        private PIServer server;

        private static readonly string scoreTagNamePrefix = "MonsterMash-score-";
        private string scoreTagName {
            get { return $"{scoreTagNamePrefix}{playerName}"; }
        }
        private PIPoint scoreTag;

        private static readonly string attackRollsTagNamePrefix = "MonsterMash-attack-rolls-";
        private string attackRollsTagName {
            get { return $"{attackRollsTagNamePrefix}{playerName}"; }
        }
        private PIPoint attackRollsTag;

        private static readonly string damageRollsTagNamePrefix = "MonsterMash-attack-rolls-";
        private string damageRollsTagName {
            get { return $"{damageRollsTagNamePrefix}{playerName}"; }
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
            // Try to create if doesn't exist
            PIPoint.TryFindPIPoint(server, scoreTagName, out scoreTag);
            if (scoreTag == null)
            {
                scoreTag = server.CreatePIPoint(scoreTagName, intAttributeProperties);
                scoreTag.UpdateValue(new AFValue() { Value = 0 }, AFUpdateOption.InsertNoCompression);
            }

            PIPoint.TryFindPIPoint(server, attackRollsTagName, out attackRollsTag);
            if (attackRollsTag == null)
            {
                attackRollsTag = server.CreatePIPoint(attackRollsTagName, intAttributeProperties);
                attackRollsTag.UpdateValue(new AFValue() { Value = 0 }, AFUpdateOption.InsertNoCompression);
            }

            PIPoint.TryFindPIPoint(server, damageRollsTagName, out damageRollsTag);
            if (damageRollsTag == null)
            {
                damageRollsTag = server.CreatePIPoint(damageRollsTagName, intAttributeProperties);
                damageRollsTag.UpdateValue(new AFValue() { Value = 0 }, AFUpdateOption.InsertNoCompression);
            }
        }

        public void UpdateScoreStat()
        {
            monstersKilled++;
            var stat = PIPoint.FindPIPoint(server, scoreTagName);
            stat.UpdateValue(new AFValue() { Value = monstersKilled }, AFUpdateOption.InsertNoCompression);
        }

        public void UpdateAttackRollStat(int attackRoll)
        {
            var stat = PIPoint.FindPIPoint(server, attackRollsTagName);
            stat.UpdateValue(new AFValue() { Value = attackRoll }, AFUpdateOption.InsertNoCompression);
        }

        public void UpdateDamageRollStat(int damageRoll)
        {
            var stat = PIPoint.FindPIPoint(server, damageRollsTagName);
            stat.UpdateValue(new AFValue() { Value = damageRoll }, AFUpdateOption.InsertNoCompression);
        }

        public Dictionary<string, int> GetScoreStats()
        {
            var stat = PIPoint.FindPIPoints(server, $"{scoreTagNamePrefix}*");

            return stat.ToDictionary((point) =>
            {
                return point.Name.Replace(scoreTagNamePrefix, "");
            }, (point) =>
            {
                return point.CurrentValue().ValueAsInt32();
            });
        }

        public Dictionary<string, int> GetAttackRollStats()
        {
            var stat = PIPoint.FindPIPoints(server, $"{attackRollsTagNamePrefix}*");

            return stat.ToDictionary((point) =>
            {
                return point.Name.Replace(attackRollsTagNamePrefix, "");
            }, (point) =>
            {
                return point.CurrentValue().ValueAsInt32();
            });
        }

        public Dictionary<string, int> GetDamageRollStats()
        {
            var stat = PIPoint.FindPIPoints(server, $"{damageRollsTagNamePrefix}*");

            return stat.ToDictionary((point) =>
            {
                return point.Name.Replace(damageRollsTagNamePrefix, "");
            }, (point) =>
            {
                return point.CurrentValue().ValueAsInt32();
            });
        }
    }
}

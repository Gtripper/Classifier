using System.Collections.Generic;

namespace Classifier
{
    public interface IMonsterFeed
    {
        List<Node> getMonster();
        Node GetMonsterFromVRICode(string vri);
    }
}
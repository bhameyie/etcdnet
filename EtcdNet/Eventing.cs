namespace EtcdNet
{
    public class Eventing
    {
        public delegate void NodeChanged(EtcdResponse update);

        public event NodeChanged OnNOdeChanged;
    }
}
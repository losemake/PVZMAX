namespace QFramework.PVZMAX 
{
    public class GameModel : AbstractModel
    {
        // ��Ϸ����
        public BindableProperty<GameMode> MGameMode = new BindableProperty<GameMode>(GameMode.START);

        // ��Ϸ��ʼ��������

        // ��Ϸѡ���������
        public BindableProperty<PlantPrefabs> PlantPrefab_1P = new BindableProperty<PlantPrefabs>(PlantPrefabs.Peashooter);
        public BindableProperty<PlantPrefabs> PlantPrefab_2P = new BindableProperty<PlantPrefabs>(PlantPrefabs.Peashooter);

        // ��Ϸ��ս��������

        protected override void OnInit()
        {
            
        }
    }
}
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

        public BindableProperty<bool> Player1_Confirm = new BindableProperty<bool>(false);
        public BindableProperty<bool> Player2_Confirm = new BindableProperty<bool>(false);

        // ��Ϸ��ս��������
        public BasePlant Player1;
        public BasePlant Player2;
        protected override void OnInit()
        {
            
        }
    }
}
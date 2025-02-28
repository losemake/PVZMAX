namespace QFramework.PVZMAX 
{
    public class GameModel : AbstractModel
    {
        // 游戏数据
        public BindableProperty<GameMode> MGameMode = new BindableProperty<GameMode>(GameMode.START);

        // 游戏开始界面数据

        // 游戏选择界面数据
        public BindableProperty<PlantPrefabs> PlantPrefab_1P = new BindableProperty<PlantPrefabs>(PlantPrefabs.Peashooter);
        public BindableProperty<PlantPrefabs> PlantPrefab_2P = new BindableProperty<PlantPrefabs>(PlantPrefabs.Peashooter);

        public BindableProperty<bool> Player1_Confirm = new BindableProperty<bool>(false);
        public BindableProperty<bool> Player2_Confirm = new BindableProperty<bool>(false);

        // 游戏对战界面数据
        public BasePlant Player1;
        public BasePlant Player2;
        protected override void OnInit()
        {
            
        }
    }
}
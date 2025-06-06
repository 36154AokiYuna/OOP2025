namespace Section02 {
    internal class Program {
        static void Main(string[] args) {

            var appVar1 = new AppVersion(5, 1);
            var appVar2 = new AppVersion(5, 1);

            if(appVar1 == appVar2) {
                Console.WriteLine("等しい");
            } else {
                Console.WriteLine("等しくない");
            }
        }
    }

    //プライマリコンストラクタを使ったクラス定義
    public record AppVersion (int m,int mi,int b = 0,int r = 0){
        public int Major { get; init; } = m;
        public int Minor { get; init; } = mi;
        public int Build { get; init; } = b;
        public int Revision { get; init; } = r;


        public override string ToString() =>
            $"{Major}.{Minor}.{Build}.{Revision}";

    }
}

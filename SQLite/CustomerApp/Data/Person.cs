using SQLite;

namespace CustomerApp {
        class Person {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            /// <summary>
            /// 名前
            /// </summary>
            public string Name { get; set; } = string.Empty;
            /// <summary>
            /// 電話番号
            /// </summary>
            public string Phone { get; set; } = string.Empty;
        /// <summary>
        ///  住所
        /// </summary>
        public string address { get; set; } = string.Empty;
        /// <summary>
        /// 写真
        /// </summary>
        public byte[] photo { get; set; }
    }
}

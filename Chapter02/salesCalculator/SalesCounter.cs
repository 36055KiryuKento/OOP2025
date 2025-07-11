﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace salesCalculator {
    //売上集計クラス
    public class SalesCounter {
        private readonly IEnumerable<Sale> _sales;



        //コンストラクタ
        public SalesCounter(string filePath) {
           _sales = ReadSales(filePath);
        }

        //店舗別の売り上げを求める
        public IDictionary<string, int> GetPerStoreSales() {
             var dict = new SortedDictionary<string, int>();
            foreach (var sale in _sales) {
                if (dict.ContainsKey(sale.ShopName)) {
                    dict[sale.ShopName] += sale.Amount;
                } else {
                    dict[sale.ShopName] = sale.Amount;
                }
            }
            return dict;
        }

            //売上データを読み込み、Saleオブジェクトのリストを返す
          public  static IEnumerable<Sale> ReadSales(string filePath) {
                //売上データを入れるリストオブジェクトを生成
               var sales = new List<Sale>();
                //ファイルを一気に読み込み
                var lines = File.ReadAllLines(filePath);
                //読み込んだ行数分繰り返し
                foreach (var line in lines) {
                    string[] items = line.Split(',');
                    //Saleオブジェクトを作成
                    var sale = new Sale() {
                        ShopName = items[0],
                        ProductCategory = items[1],
                        Amount = int.Parse(items[2])
                    };
                    sales.Add(sale);
                }
                return sales;
            }
        }
    }
    

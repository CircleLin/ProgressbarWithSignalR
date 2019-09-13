# ProgressbarWithSignalR

> 這是一個使用SignalR技術，來取得匯入Excel處理進度的sample code

#### 情境設計
User匯入Excel資料並存到資料庫，並且將進度顯示在畫面上


#### 實作概念步驟

1.  前端選擇Excel檔案並匯入，檔案送出時，開啟與SignalR的連線
2.  後端取出Excel資料，將總筆數與存到Cache
3.  使用foreach將每筆Excel資料存到資料庫，將目前筆數存到Cache
4.  每秒讀取Cache中的目前筆數與總筆數，相除算出百分比進度，SignalR將進度推播至前端
5.  前端接收後以Bootstrap的progress bar顯示

請使用 [台北市政府公開資料 臺北市預防接種合約院所] Excel匯入

  [台北市政府公開資料 臺北市預防接種合約院所]: https://data.taipei/api/getDatasetInfo/downloadResource?id=ec201f0a-2efa-4426-9439-a8daea7b33c7&rid=3063803c-8794-4d19-ab1c-3e602dd77506        "台北市政府公開資料 臺北市預防接種合約院所"


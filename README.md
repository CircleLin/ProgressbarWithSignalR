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

* * *
請使用

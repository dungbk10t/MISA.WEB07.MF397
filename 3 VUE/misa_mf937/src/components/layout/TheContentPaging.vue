<template>
  <div class="content-paging">
    <div class="paging-left">
      <p>
        Hiển thị <b>{{ startRecord }}-{{ endRecord }}/{{ totalRecord }}</b> Nhân
        viên
      </p>
    </div>
    <div class="paging-center">
      <Button
        buttonClass="btn-select-prev-list-page button-paging"
        @btnClick="btnDoubleBackOnClick"
      />
      <Button
        buttonClass="btn-select-prev-page button-paging"
        @btnClick="btnBackOnClick"
      />
      <Button
        buttonClass="page-number"
        v-for="currentShowPage in currentShowPages"
        :key="currentShowPage"
        :buttonText="currentShowPage + ''"
        :class="{ button_actived: currentShowPage == currentPage }"
        @btnClick="$emit('changeCurrentPage', currentShowPage)"
      />
      <Button
        buttonClass="btn-select-next-page button-paging"
        @btnClick="btnNextOnClick"
      />
      <Button
        buttonClass="btn-select-next-list-page button-paging"
        @btnClick="btnDoubleNextOnClick"
      />
    </div>
    <div class="paging-right">
      <p>
        <b>{{ pagingSize }}</b> nhân viên/trang
      </p>
      <button @click="pageSizeIncrease"><i class="fas fa-angle-up"></i></button>
      <button @click="pageSizeDecrease"><i class="fas fa-angle-down"></i></button>
    </div>
  </div>
</template>

<script>
import Button from "../../components/base/BaseButton.vue";
export default {
  name: "TheContentPaging",
  components: {
    Button,
  },
  props: {
    totalRecord: Number,
    pagingSize: {
      default: 10,
      type: Number,
    },
    currentPage: Number,
  },
  data() {
    return {
      startRecord: 0,
      endRecord: 0,
      currentShowPages: [],
      totalPage: 0,
    };
  },

  methods: {
    /**
     * Hàm thay cập nhật tất cả các thông tin về :
     * bản ghi bắt đầu, bản ghi kết thúc, tổng số bản ghi, số bản ghi trên 1 trang
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    changePage() {
      console.log("1"+this.currentShowPages)
      // 1. Cập nhật lại giá trị bản ghi bắt đầu và bản ghi kết thúc.
      // Ví dụ : Có 200 bản ghi, 1 trang có 30 bản ghi, khi đó :
      // Bản ghi bắt đầu ở page (3) : 61 = 30 * (3 - 1) + 1
      this.startRecord = this.pagingSize * (this.currentPage - 1) + 1;
      // Bản ghi kết thúc ở page (3) : 90 = 30 * 3
      this.endRecord = this.pagingSize * this.currentPage;
      // Nếu giá trị bản ghi cuối mà lớn hơn tổng số bản ghi hiện tại thì cập nhật
      if (this.endRecord > this.totalRecord) {
        this.endRecord = this.totalRecord;
      }
      // 2. Cập nhật lại mảng currentShowPage tức là mảng lưu page từ start đến end.
      // Tổng sổ trang = chia lấy phần nguyên của (tổng số bản ghi) / (Số bản ghi trên 1 trang)
      this.totalPage = Math.ceil(this.totalRecord / this.pagingSize);
      // Có 3 trường hợp :
      // Trường hợp 1 : Hiển thị 7 bản ghi từ 1->7

      if (this.totalPage <= 7) {
        this.currentShowPages = [];
        for (let i = 1; i <= this.totalPage; i++) {
          this.currentShowPages.push(i);
        }
      } else {
        this.currentShowPages = [];
        // Trường hợp 2 :

        if (this.totalPage - this.currentPage >= 3 && this.currentPage >= 4) {
          let start = this.currentPage - 3;
          for (let i = start; i <= start + 6; i++) {
            this.currentShowPages.push(i);
          }
        }
        // Trường hợp 3 :
        else if (this.currentPage <= 3) {
          this.currentShowPages = [1, 2, 3, 4, 5, 6, 7];
        }
        // Trường hợp 4 :
        else {
          let start = this.totalPage - 6;
          for (let i = start; i <= start + 6; i++) {
            this.currentShowPages.push(i);
          }
        }
        // console.log(this.currentShowPages)
      }
    },
    /**
     * Sự kiện click vào để hiển thị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    btnDoubleBackOnClick() {
      this.$emit("changeCurrentPage", 1);
    },
    /**
     * Sự kiện click vào để hiển thị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    btnBackOnClick() {
      if (this.currentPage > 1) {
        this.$emit("changeCurrentPage", this.currentPage - 1);
      }
    },
    /**
     * Sự kiện click vào để hiển thị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    btnNextOnClick() {
      if (this.currentPage < this.totalPage) {
        this.$emit("changeCurrentPage", this.currentPage + 1);
      }
    },
    /**
     * Sự kiện click vào để hiển thị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    btnDoubleNextOnClick() {
      this.$emit("changeCurrentPage", this.totalPage);
    },
    /**
     * Sự kiện click vào nút tăng ở paging để tăng "số bản ghi/ trang" thêm 10 đơn vị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    pageSizeIncrease() {
      // Tăng "số bản ghi/ trang" thêm 10 đơn vị
      this.$emit("changePagingSize", this.pagingSize + 10);
      // Cập nhật trang hiện tại là trang 1
      this.$emit("changeCurrentPage", 1);
    },
    /**
     * Sự kiện click vào nút tăng ở paging để giảm "số bản ghi/ trang" đi 10 đơn vị
     * Created : Phạm Tuấn Dũng (13/08/2021)
     */
    pageSizeDecrease() {
      // Giảm "số bản ghi/ trang" thêm 10 đơn vị
      this.$emit("changePagingSize", this.pagingSize - 10);
      // Cập nhật trang hiện tại là trang 1
      this.$emit("changeCurrentPage", 1);
    },
  },
  watch:{
    currentPage(){
      this.changePage();
    },
    pagingSize(){
      this.changePage();
    },
    totalRecord(){
      this.changePage();
    } 
  }
};
</script>

<style scoped>
.content-paging {
  width: 100%;
  height: 56px;
  position: relative;
  float: left;
  display: flex;
  align-items: center;
}

.content-paging .paging-left {
  width: 250px;
  left: 0;
  top: 0;
  position: absolute;
  height: 100%;
}

.content-paging .paging-left p{
  line-height: 100%;
}

.content-paging .paging-right {
  width: 160px;
  right: 0;
  top: 8px;
  position: absolute;
  height: 40px;
  background-color: #fff;
  border-radius: 4px;
}

.content-paging .paging-right p {
  line-height: 100%;
  width: 130px;
  display: block;
  float: left;
  padding-left: 10px;
  box-sizing: border-box;
}

.content-paging .paging-right button {
  height: 50%;
  width: 30px;
  float: left;
  outline: none;
  border: none;
  background-color: #fff;
}

.content-paging .paging-center{
  width: auto;
  height: 100%;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate( -50%, -50% );
  margin: 0;
  display: flex;
  align-items: center;
}

.content-paging .paging-center button{
  height: 32px;
  margin: auto 5px;
  background-repeat: no-repeat;
  background-position: center center;
  width: 32px;

}

.button-paging:hover{
width: 32px;
background-color: #fff;
border-radius: 4px ;
}

.page-number{
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>
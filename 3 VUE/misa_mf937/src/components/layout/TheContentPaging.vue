<template>
    <div class="content-paging">
            <div class="paging-bar">
                <div class="paging-record-info"><p>Hiển thị <b>1-10/10000</b> Nhân viên hàng</p></div>
                <div class="paging-option">
                    <div class="btn-select-prev-list-page"></div>
                    <div class="btn-select-prev-page"></div>
                    <div class="list-page">
                        <button class="page-number">1</button>
                        <button class="page-number">2</button>
                        <button class="page-number">3</button>
                        <button class="page-number">4</button>
                    </div>
                    <div class="btn-select-next-page"></div>
                    <div class="btn-select-next-list-page"></div>
                </div>
                <div class="paging-record-option">10 Nhân viên trên trang</div>
            </div>
    </div>
</template>

<script>
export default {
    name: 'TheContentPaging',
    components: {
        
    }
}
</script>
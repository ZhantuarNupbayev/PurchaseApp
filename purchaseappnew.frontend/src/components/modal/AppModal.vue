<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div
        class="modal"
        role="dialog"
        aria-labelledby="modal-title"
        aria-describedby="modal-desc"
      >
        <header class="modal-header" id="modal-title">
          <slot name="header"></slot>
        </header>

        <section class="modal-body" id="modal-desc">
          <slot name="body"></slot>
        </section>

        <footer class="modal-footer">
          <slot name="footer"></slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  name: "AppModal",
  components: {},
})
export default class AppModal extends Vue {
  close() {
    this.$emit("close");
  }
}
</script>

<style lang="scss" scoped>
@import "@/scss/global.scss";
.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: #fff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  width: 640px;
  border-radius: 3px;
}

.modal-header {
  padding: 15px;
  display: flex;
  background-color: $app-color-blue;
  color: #fff;
  font-size: 1.2rem;
  font-weight: bold;
  border-bottom: 1px solid #eee;
  justify-content: space-between;
}

.modal-footer {
  padding: 12px;
  display: flex;
  border-top: 1px solid #eee;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 2.4rem;
}

.modal-fade-enter,
.modal-fade-leave-active {
  opacity: 0;
}

.modal-fade-enter-active,
.modal-fade-leave-active {
  transition: opacity 0.5s ease;
}
</style>
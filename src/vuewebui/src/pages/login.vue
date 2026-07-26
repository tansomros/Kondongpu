<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const username = ref('')
const password = ref('')
const isPasswordVisible = ref(false)

const login = () => {
  // Logic to handle login
  // For now, redirect to dashboards
  router.push('/dashboards')
}
</script>

<template>
  <div class="auth-wrapper d-flex align-center justify-center pa-4">
    <VCard
      class="auth-card pa-4 pt-7"
      max-width="448"
    >
      <VCardItem class="justify-center">
        <VCardTitle class="font-weight-bold text-h4 py-1 text-primary">
          Kondongpu
        </VCardTitle>
      </VCardItem>

      <VCardText class="pt-2">
        <h5 class="text-h5 font-weight-semibold mb-1">
          Welcome to Kondongpu! 👋🏻
        </h5>
        <p class="mb-0">
          Please sign-in to your account and start the adventure
        </p>
      </VCardText>

      <VCardText>
        <VForm @submit.prevent="login">
          <VRow>
            <!-- username -->
            <VCol cols="12">
              <VTextField
                v-model="username"
                label="Username"
                type="text"
                placeholder="admin"
              />
            </VCol>

            <!-- password -->
            <VCol cols="12">
              <VTextField
                v-model="password"
                label="Password"
                placeholder="············"
                :type="isPasswordVisible ? 'text' : 'password'"
                :append-inner-icon="isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'"
                @click:append-inner="isPasswordVisible = !isPasswordVisible"
              />

              <!-- remember me checkbox -->
              <div class="d-flex align-center justify-space-between flex-wrap mt-1 mb-4">
                <VCheckbox label="Remember me" />
                <a class="text-primary text-decoration-none" href="javascript:void(0)">Forgot Password?</a>
              </div>

              <!-- login button -->
              <VBtn
                block
                type="submit"
                color="primary"
              >
                Login
              </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </div>
</template>

<route lang="yaml">
meta:
  layout: blank
  public: true
</route>

<style scoped>
.auth-wrapper {
  min-height: 100vh;
  background-color: rgb(var(--v-theme-background));
}
.auth-card {
  width: 100%;
}
</style>

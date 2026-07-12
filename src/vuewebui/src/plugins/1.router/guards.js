import { useAbility } from "@casl/vue"
import { userService } from "../auth"

export const setupGuards = router => {
 
  router.beforeEach(async to => {
    if(to.meta.public) return 

    if(to.path === "/signin-oidc") return 

    const ability = useAbility()
    const user = getUserFromStorage()

    if(!user) {
      clearUserData()
      await userService.signin()
      
      return false
    }

    const isTokenExpired = user.expires_at && new Date() > new Date(user.expires_at * 1000)
    if(isTokenExpired) {
      try {
        const refreshUser = await userService.signinSilent()

        saveUserToStorage(refreshUser)
        updateAbility()
      } catch(error) {
        clearUserData()
        await userService.signin()
        
        return false
      }
    } else {
      updateAbility(ability)
    }
  })
}

function getUserFromStorage() {
  const userJson = localStorage.getItem("userData")  
  
  return userJson ? JSON.parse(userJson) : null
}

function saveUserToStorage(user) {
  localStorage.setItem("userData", JSON.stringify(user))
  localStorage.setItem("accessToken", user.access_token)
  useCookie("userData").value = user
  useCookie("accessToken").value = user.access_token
}

function clearUserData() {
  localStorage.removeItem("userData")
  localStorage.removeItem("accessToken")
  useCookie("userData").value = null
  useCookie("accessToken").value = null
  useCookie("userAbilityRules").value = null
}

function updateAbility(ability) {
  const userAbilityRules = [{ action: 'manage', subject: 'all' }]

  ability.update(userAbilityRules)
  useCookie("userAbilityRules").value = { userAbilityRules }
}

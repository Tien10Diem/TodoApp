import { defineStore } from 'pinia'

export const buttonT = defineStore('btRandL', {
  state: () => ({
    buttonText: 'Register'
  }),
  actions: {
    setButtonText(text: string) {
      this.buttonText = text
    }
  }
})
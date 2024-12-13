﻿using System.Collections;
using TMPro;
using UnityEngine;

namespace Gamekit3D
{
    public class DialogueCanvasController : MonoBehaviour
    {
        #region Variables

        public Animator animator;
        public TextMeshProUGUI textMeshProUGUI;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");

        protected Coroutine m_DeactivationCoroutine;

        #endregion

        #region Public methods

        public void ActivateCanvasWithText(string text)
        {
            if (m_DeactivationCoroutine != null)
            {
                StopCoroutine(m_DeactivationCoroutine);
                m_DeactivationCoroutine = null;
            }

            gameObject.SetActive(true);
            animator.SetBool(m_HashActivePara, true);
            textMeshProUGUI.text = text;
        }

        public void ActivateCanvasWithTranslatedText(string phraseKey)
        {
            if (m_DeactivationCoroutine != null)
            {
                StopCoroutine(m_DeactivationCoroutine);
                m_DeactivationCoroutine = null;
            }

            gameObject.SetActive(true);
            animator.SetBool(m_HashActivePara, true);
            textMeshProUGUI.text = Translator.Instance[phraseKey];
        }

        public void DeactivateCanvasWithDelay(float delay)
        {
            m_DeactivationCoroutine = StartCoroutine(SetAnimatorParameterWithDelay(delay));
        }

        #endregion

        #region Private methods

        private IEnumerator SetAnimatorParameterWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetBool(m_HashActivePara, false);
        }

        #endregion
    }
}
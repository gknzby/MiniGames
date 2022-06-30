using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    public class Photographer : MonoBehaviour
    {
        [SerializeField] private Camera photoCamera; 
        [SerializeField] private RenderTexture photographTex;
        [SerializeField] private List<RenderTexture> photoGallery = new();
        public List<RenderTexture> PhotoGallery { get { return photoGallery; } }

        [ContextMenu("CEK FOTOMU CEK")]
        public void TakePhoto()
        {
            RenderTexture newPhoto = new RenderTexture(photographTex);
            Graphics.Blit(photographTex, newPhoto);
            photoGallery.Add(newPhoto);
        }

        public IEnumerator TakePhoto(float waitToTake)
        {
            yield return new WaitForSeconds(waitToTake);
            photoCamera.gameObject.SetActive(true);
            yield return null;
            TakePhoto();
            yield return null;
            photoCamera.gameObject.SetActive(false);
        }

        public void ClearGallery()
        {
            foreach(RenderTexture renderTexture in photoGallery)
            {
                renderTexture.Release();
            }

            photoGallery.Clear();
        }
    }
}

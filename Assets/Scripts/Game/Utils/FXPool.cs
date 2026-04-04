using Game.Tiles;
using ResourcesLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Utils
{
    public class FXPool
    {
        private readonly List<GameObject> _fxPool = new List<GameObject>();
        private GameObject _prefabFX;
        private IObjectResolver _objectResolver;
        private readonly GameResourcesLoader _gameResourcesLoader;

        public FXPool(IObjectResolver objectResolver, GameResourcesLoader gameResourcesLoader)
        {
            _objectResolver = objectResolver;
            _gameResourcesLoader = gameResourcesLoader;
        }

        public GameObject GetFXFromPool(Vector3 position, Transform transform)
        {
            for (int i = 0; i < _fxPool.Count; i++)
            {
                if (_fxPool[i].activeInHierarchy) continue;
                _fxPool[i].gameObject.transform.position= position;
                _fxPool[i].SetActive(true);
                return _fxPool[i];
            }
            var FXGameobject = CreateFXObject(position, transform);
            FXGameobject.SetActive(true);
            return FXGameobject;

        }

        private GameObject CreateFXObject(Vector3 position, Transform transform)
        {
           var FxObject = _objectResolver.Instantiate(_gameResourcesLoader.FxPrefab, 
                position + Vector3.forward, Quaternion.identity, transform);
            _fxPool.Add(FxObject);
            return FxObject;
        }

    }
}


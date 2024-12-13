using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Zenject.Asteroids
{
    public class ShipStateDead : ShipState
    {
        #region Public Nested Types

        [Serializable]
        public class Settings
        {
            #region Variables

            public float explosionForce;

            #endregion
        }

        public class Factory : PlaceholderFactory<ShipStateDead> { }

        #endregion

        #region Variables

        private readonly BrokenShipFactory _brokenShipFactory;
        private readonly ExplosionFactory _explosionFactory;
        private readonly Settings _settings;
        private readonly Ship _ship;
        private readonly SignalBus _signalBus;
        private GameObject _explosion;

        private GameObject _shipBroken;

        #endregion

        #region Setup/Teardown

        public ShipStateDead(
            Settings settings, Ship ship,
            ExplosionFactory explosionFactory,
            BrokenShipFactory brokenShipFactory,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _brokenShipFactory = brokenShipFactory;
            _explosionFactory = explosionFactory;
            _settings = settings;
            _ship = ship;
        }

        #endregion

        #region Public methods

        public override void Dispose()
        {
            _ship.MeshRenderer.enabled = true;

            _ship.ParticleEmitter.gameObject.SetActive(true);

            Object.Destroy(_explosion);
            Object.Destroy(_shipBroken);
        }

        public override void Start()
        {
            _ship.MeshRenderer.enabled = false;

            _ship.ParticleEmitter.gameObject.SetActive(false);

            _explosion = _explosionFactory.Create().gameObject;
            _explosion.transform.position = _ship.Position;

            _shipBroken = _brokenShipFactory.Create().gameObject;
            _shipBroken.transform.position = _ship.Position;
            _shipBroken.transform.rotation = _ship.Rotation;

            foreach (Rigidbody rigidBody in _shipBroken.GetComponentsInChildren<Rigidbody>())
            {
                float randomTheta = Random.Range(0, Mathf.PI * 2.0f);
                Vector3 randomDir = new Vector3(Mathf.Cos(randomTheta), Mathf.Sin(randomTheta), 0);
                rigidBody.AddForce(randomDir * _settings.explosionForce);
            }

            _signalBus.Fire<ShipCrashedSignal>();
        }

        public override void Update() { }

        #endregion
    }
}
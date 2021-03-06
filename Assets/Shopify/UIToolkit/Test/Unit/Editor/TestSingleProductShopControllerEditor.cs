namespace Shopify.UIToolkit.Test.Unit {
    using UnityEngine;
    using UnityEditor;
    using UnityEngine.TestTools;
    using NSubstitute;
    using NUnit.Framework;
    using Shopify.UIToolkit.Editor;
    using Shopify.Unity.SDK;

    [TestFixture]
    public class TestSingleProductShopControllerEditor {
        private SingleProductShopControllerEditor _editor;
        private SingleProductShopController _controller;
        private GameObject _gameObject;

        [SetUp]
        public void Setup() {
            _gameObject = new GameObject("TestSingleProductShopControllerEditor");
            _controller = _gameObject.AddComponent<SingleProductShopController>();
            _controller.Credentials = new ShopCredentials("","");

            _editor = Editor.CreateEditor(_controller) as SingleProductShopControllerEditor;
            _editor.View = Substitute.For<ISingleProductShopControllerEditorView>();
        }

        [TearDown]
        public void TearDown() {
            GameObject.DestroyImmediate(_gameObject);
        }

        [Test]
        public void TestNotVerifiedCredentialsDoesNotShowProductPicker() {
            _controller.Credentials.State = ShopCredentials.VerificationState.Unverified;
            _editor.OnEnable();
            _editor.OnInspectorGUI();
            _editor.View.DidNotReceive().DrawProductPicker();
        }

        [Test]
        public void TestVerifiedCredentialsDoesShowProductPicker() {
            _controller.Credentials.State = ShopCredentials.VerificationState.Verified;
            _editor.OnEnable();
            _editor.OnInspectorGUI();
            _editor.View.Received().DrawProductPicker();
        }

        [Test]
        public void TestInvalidCredentialsDoesNotShowProductPicker() {
            _controller.Credentials.State = ShopCredentials.VerificationState.Invalid;
            _editor.OnEnable();
            _editor.OnInspectorGUI();
            _editor.View.DidNotReceive().DrawProductPicker();
        }
    }
}
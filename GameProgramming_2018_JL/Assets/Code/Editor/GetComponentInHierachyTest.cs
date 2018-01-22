using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace TankGame.Testing
{
    public class GetComponentInHierachyTest
    {
        private GameObject _parent;
        private GameObject _child;
        private GameObject _grandChild;

        private GetComponentInHierarchyTester Setup(bool includeInActive, bool componentInParent, bool setActive)
        {
            _parent = new GameObject();
            _child = new GameObject();
            _grandChild = new GameObject();

            _child.transform.parent = _parent.transform;
            _grandChild.transform.parent = _child.transform;

            GetComponentInHierarchyTester tester = _child.AddComponent<GetComponentInHierarchyTester>();

            tester.Setup(includeInActive, componentInParent, setActive);

            return tester;
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInChild_IncludeInActive_SetActive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: true, componentInParent: false, setActive: true);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInChild_DontIncludeInActive_SetActive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: false, componentInParent: false, setActive: true);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInChild_IncludeInActive_SetInactive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: true, componentInParent: false, setActive: false);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInChild_DontIncludeInActive_SetInactive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: false, componentInParent: false, setActive: false);
            TestComponent result = tester.Run();
            Assert.IsNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInParent_IncludeInActive_SetActive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: true, componentInParent: true, setActive: true);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInParent_DontIncludeInActive_SetActive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: false, componentInParent: true, setActive: true);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInParent_IncludeInActive_SetInactive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: true, componentInParent: true, setActive: false);
            TestComponent result = tester.Run();
            Assert.NotNull(result);
        }

        [Test]
        public void GetComponentInHierarchyTest_ComponentInParent_DontIncludeInActive_SetInactive()
        {
            GetComponentInHierarchyTester tester = Setup
                (includeInActive: false, componentInParent: true, setActive: false);
            TestComponent result = tester.Run();
            Assert.IsNull(result);
        }
    }
}

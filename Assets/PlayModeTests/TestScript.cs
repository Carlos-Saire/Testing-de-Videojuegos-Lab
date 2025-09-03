using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestScript
{
    [Test]
    public void TestScriptSimplePasses()
    {
        Assert.Equals(0,0);
    }
    [UnityTest]
    public IEnumerator TestScriptWithEnumeratorPasses()
    {
        yield return null;
    }
}

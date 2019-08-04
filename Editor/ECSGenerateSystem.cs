using System.IO;
using UnityEngine;
using UnityEditor;

public static class ECSGenerateSystem
{
    static string _template =
@"using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class ##SystemName## : JobComponentSystem
{
    struct Job : IJobForEach<Translation>
    {
        public void Execute(ref Translation t)
        {

        }
    }

    EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(new EntityQueryDesc
        {
            All = new[] { new ComponentType(typeof(Translation)) },
        });
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
        => new Job().Schedule(_query, inputDeps);
}
";

    class DoCreateSystem : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string baseFile = Path.GetFileNameWithoutExtension(pathName);
            string fullPath = Path.GetFullPath(pathName);

            var text = _template.Replace("##SystemName##", baseFile);
            File.WriteAllText(fullPath, text);

            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(
                AssetDatabase.LoadAssetAtPath(pathName, typeof(Object)));
        }
    }

    [MenuItem("Assets/Create/ECS System", false, 0)]
    public static void Create()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateSystem>(),
            "NewSystem.cs",
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            "");
    }
}
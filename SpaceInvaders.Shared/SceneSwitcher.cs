using System.Collections.Generic;

namespace SpaceInvaders.Shared
{
    public class SceneSwitcher
    {
        private List<BaseScene> scenes;
        private BaseScene currentScene;

        public SceneSwitcher()
        {
            this.scenes = new List<BaseScene>();
        }

        public void SwitchScene<T>() where T : BaseScene
        {
            foreach (var scene in this.scenes)
            {
                if (scene is T)
                {
                    this.currentScene.Enabled = false;
                    this.currentScene.Visible = false;
                    this.currentScene = scene;
                    scene.Enabled = true;
                    scene.Visible = true;
                    return;
                }
            }

            throw new System.Exception("Scene not found");
        }

        public void AddScene(BaseScene scene)
        {
            this.scenes.Add(scene);

            if (scenes.Count == 1)
            {
                // set first scene as current
                this.currentScene = scene;
            }
        }
    }
}

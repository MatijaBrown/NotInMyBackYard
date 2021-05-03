using NIMBY.Graphics;
using NIMBY.Ui;
using NIMBY.World;
using System;
using System.Collections.Generic;
using System.IO;

namespace NIMBY.States
{
    public class LevelSelectorState : IState
    {

        private const string SAVE_GAME = "./Assets/Levels/levels.meta";

        private readonly IList<LevelData> _levelMetas = new List<LevelData>();
        private readonly IList<LevelSelectButton> _selectButtons = new List<LevelSelectButton>();

        private StateManager _manager;
        private LevelSelectorStateRenderer _renderer;

        private int _levelCount = 0;
        private Texture _star;

        public StateManager Manager => _manager;

        public Texture Star => _star;

        public void Init(StateManager manager)
        {
            _manager = manager;
            LoadLevelsFromFile();
        }

        private void LoadLevelsFromFile()
        {
            int bestTime = 0;
            short bestRating = 0;
            string name = "";

            string text = File.ReadAllText(SAVE_GAME);
            string[] lines = text.Split(Environment.NewLine);
            _levelCount = int.Parse(lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');

                if (line[0].StartsWith("NEXT"))
                {
                    LevelData data = new();
                    data.bestTime = bestTime;
                    data.bestRating = bestRating;
                    data.name = name;
                    _levelMetas.Add(data);
                }
                else if (line[0].StartsWith("BestTime"))
                {
                    bestTime = int.Parse(line[1]);
                }
                else if (line[0].StartsWith("BestRating"))
                {
                    bestRating = short.Parse(line[1]);
                }
                else if (line[0].StartsWith("Name"))
                {
                    name = line[1];
                }
            }
        }

        public void Start()
        {
            _renderer = new LevelSelectorStateRenderer(this);

            float offset = _manager.Game.Witdh / 2.0f - 4 * 300 / 2.0f + 50.0f;
            float x = offset;
            float y = 25.0f;
            for (int i = 0; i < _levelCount; i++)
            {
                _selectButtons.Add(new LevelSelectButton(x,
                    y, new System.Numerics.Vector3(0.2f, 0.6f, 0.6f), _levelMetas[i], this));

                x += 300.0f;
                if ((x - offset) / 300.0f == 4.0f)
                {
                    x = offset;
                    y += 300.0f;
                }
            }

            _star = ResourceManager.LoadTexture("Sun");
        }

        public void Update(float delta)
        {
            foreach (LevelSelectButton button in _selectButtons)
            {
                button.Update();
            }
        }

        public void Render()
        {
            foreach (LevelSelectButton button in _selectButtons)
            {
                button.Render(_renderer, _manager.Game.Fons);
            }
        }

        public void Stop()
        {
            Input.OnMouseReleased = null;
            Input.OnKeyReleased = null;
            ResourceManager.Clear();
            _selectButtons.Clear();

            GC.Collect();
        }

        public void Dispose()
        {
            string output = "";
            output += _levelMetas.Count.ToString() + Environment.NewLine;
            for (int i = 0; i < _levelMetas.Count; i++)
            {
                var meta = _levelMetas[i];
                output += "Name " + meta.name + Environment.NewLine;
                output += "BestTime " + meta.bestTime + Environment.NewLine;
                output += "BestRating " + meta.bestRating + Environment.NewLine;
                output += "NEXT" + Environment.NewLine;
            }
            output = output.Remove(output.Length - 1);

            File.WriteAllText(SAVE_GAME, output);
        }
    }
}

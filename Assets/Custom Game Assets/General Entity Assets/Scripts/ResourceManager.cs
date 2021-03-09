using UnityEngine;
using UnityEngine.Audio;

public class ResourceManager
{
    public class Sources
    {
        private static string folder = "Sources/";
        public class Spells
        {
            private static string folder = Sources.folder + "Spells/";
            public static ParticleSystem Default = ((GameObject)Resources.Load(folder + "Default Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem DefaultStationary = ((GameObject)Resources.Load(folder + "Default Stationary Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem Fire = ((GameObject)Resources.Load(folder + "Default Fire Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem Lightning = ((GameObject)Resources.Load(folder + "Default Lightning Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem Smoke = ((GameObject)Resources.Load(folder + "Default Smoke Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem Earth = ((GameObject)Resources.Load(folder + "Default Earth Source", typeof(GameObject))).GetComponent<ParticleSystem>();
            public static ParticleSystem Ice = ((GameObject)Resources.Load(folder + "Default Ice Source", typeof(GameObject))).GetComponent<ParticleSystem>();
        }

        public class SwordEffects
        {
            private static string folder = Sources.folder + "Sword Skills/";
            public static GameObject Fire = ((GameObject)Resources.Load(folder + "Fire Source", typeof(GameObject)));
            public static GameObject Ice = ((GameObject)Resources.Load(folder + "Ice Source", typeof(GameObject)));
            public static GameObject Lightning = ((GameObject)Resources.Load(folder + "Lightning Source", typeof(GameObject)));
            public static GameObject Earth = ((GameObject)Resources.Load(folder + "Earth Source", typeof(GameObject)));
        }
    }

    public class Effects
    {
        private static string folder = "Effects/";
        public static ParticleSystem Burning = ((GameObject)Resources.Load(folder + "Burning Effect", typeof(GameObject))).GetComponent<ParticleSystem>();
        public static ParticleSystem Electrified = ((GameObject)Resources.Load(folder + "Electrified Effect", typeof(GameObject))).GetComponent<ParticleSystem>();
        public static ParticleSystem Frozen = ((GameObject)Resources.Load(folder + "Frozen Effect", typeof(GameObject))).GetComponent<ParticleSystem>();
        public static ParticleSystem Hit = ((GameObject)Resources.Load(folder + "Hit Effect", typeof(GameObject))).GetComponent<ParticleSystem>();
        public static ParticleSystem Sparks = ((GameObject)Resources.Load(folder + "Sparks", typeof(GameObject))).GetComponent<ParticleSystem>();

        public class Sword
        {
            private static string folder = Effects.folder + "Sword/";
            public class Trails
            {
                private static string folder = Sword.folder + "Trails/";
                public static SwingTrailRenderer Default = ((GameObject)Resources.Load(folder + "Sword Default Trail", typeof(GameObject))).GetComponent<SwingTrailRenderer>();
                public static SwingTrailRenderer Fire = ((GameObject)Resources.Load(folder + "Sword Fire Trail", typeof(GameObject))).GetComponent<SwingTrailRenderer>();
                public static SwingTrailRenderer Ice = ((GameObject)Resources.Load(folder + "Sword Ice Trail", typeof(GameObject))).GetComponent<SwingTrailRenderer>();
                public static SwingTrailRenderer Lightning = ((GameObject)Resources.Load(folder + "Sword Lightning Trail", typeof(GameObject))).GetComponent<SwingTrailRenderer>();
                public static SwingTrailRenderer Earth = ((GameObject)Resources.Load(folder + "Sword Earth Trail", typeof(GameObject))).GetComponent<SwingTrailRenderer>();
            }
        }
    }

    public class Components
    {
        private static string folder = "Components/"; 
        public static Arc Arc = ((GameObject)Resources.Load(folder + "Arc", typeof(GameObject))).GetComponent<Arc>();
        public static GameObject IndicatorBase = (GameObject)Resources.Load(folder + "Quad Base Indicator", typeof(GameObject));
    }

    public class Materials
    {
        private static string folder = "Materials/";
        public static Material LightningArc1 = (Material)Resources.Load(folder + "Lightning Arc 1", typeof(Material));
        public static Material LightningArc2 = (Material)Resources.Load(folder + "Lightning Arc 2", typeof(Material));
        public static Material IndicatorCircleAOE = (Material)Resources.Load(folder + "AOE Circle Indicator Material", typeof(Material));
        public static Material IndicatorSquareAOE = (Material)Resources.Load(folder + "AOE Square Indicator Material", typeof(Material));
        public static Material IndicatorTriangleAOE = (Material)Resources.Load(folder + "AOE Triangle Indicator Material", typeof(Material));
        public static Material IndicatorCirlceRange = (Material)Resources.Load(folder + "Range Circle Indicator Material", typeof(Material));
        public static Material DefaultMaterial = (Material)Resources.Load(folder + "Default Material", typeof(Material));

        public class Resistances
        {
            private static string folder = Materials.folder + "Resistances/";
            public static Material Fire = (Material)Resources.Load(folder + "Fire Resistance", typeof(Material));
            public static Material Ice = (Material)Resources.Load(folder + "Ice Resistance", typeof(Material));
            public static Material Lightning = (Material)Resources.Load(folder + "Lightning Resistance", typeof(Material));
            public static Material Physical = (Material)Resources.Load(folder + "Physical Resistance", typeof(Material));
        }
    }

    public class UI
    {
        private static string folder = "UI/";
        public static GameObject SkillListButton = ((GameObject)Resources.Load(folder + "Skill Button", typeof(GameObject)));
        public static GameObject SkillListColumn = ((GameObject)Resources.Load(folder + "Skill List Column", typeof(GameObject)));
        public static GameObject EffectDisplay = ((GameObject)Resources.Load(folder + "Effect Display", typeof(GameObject)));

        public class Sounds
        {
            private static string folder = UI.folder + "Sounds/";
            public static AudioClip ButtonHoverEnter = (AudioClip)Resources.Load(folder + "Button Hover Enter", typeof(AudioClip));
        }
    }

    public class Audio
    {
        private static string folder = "Audio/";

        public class AudioSources
        {
            public enum Range
            {
                Short,
                Mid,
                Long
            }

            private static string folder = Audio.folder + "Audio Sources/";
            public static AudioSource ShortRange = (AudioSource)Resources.Load(folder + "Short Range", typeof(AudioSource));
            public static AudioSource MidRange = (AudioSource)Resources.Load(folder + "Mid Range", typeof(AudioSource));
            public static AudioSource LongRange = (AudioSource)Resources.Load(folder + "Long Range", typeof(AudioSource));

            public static AudioSource LoadAudioSource(string mixer, AudioSource audioSource, Range range)
            {
                audioSource.playOnAwake = false;
                audioSource.loop = false;
                audioSource.outputAudioMixerGroup = AudioMixers.MainMixer.FindMatchingGroups(mixer)[0];
                audioSource.rolloffMode = AudioRolloffMode.Custom;
                audioSource.spatialBlend = 1f;
                switch (range)
                {
                    case Range.Mid:
                        audioSource.maxDistance = MidRange.maxDistance;
                        audioSource.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, MidRange.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
                        return audioSource;
                    case Range.Long:
                        audioSource.maxDistance = LongRange.maxDistance;
                        audioSource.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, LongRange.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
                        return audioSource;
                    default:
                        audioSource.maxDistance = ShortRange.maxDistance;
                        audioSource.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, ShortRange.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
                        return audioSource;
                }
            }
        }

        public class AudioMixers
        {
            private static string folder = Audio.folder + "Audio Mixers/";
            public static AudioMixer MainMixer = (AudioMixer)Resources.Load(folder + "Main Mixer", typeof(AudioMixer));
        }

        public class Ambient
        {
            private static string folder = Audio.folder + "Ambient/";
            public static AudioClip CaveBackground = (AudioClip)Resources.Load(folder + "Cave Audio", typeof(AudioClip));
            public static AudioClip CityBackground = (AudioClip)Resources.Load(folder + "City Audio", typeof(AudioClip));
            public static AudioClip CombatBackground = (AudioClip)Resources.Load(folder + "Combat Audio", typeof(AudioClip));
            public static AudioClip ForestBackground = (AudioClip)Resources.Load(folder + "Forest Audio", typeof(AudioClip));
        }

        public class Footsteps
        {
            private static string folder = Audio.folder + "Footsteps/";
            public static AudioClip WalkForestLight = (AudioClip)Resources.Load(folder + "Light Forest", typeof(AudioClip));
            public static AudioClip WalkForestHeavy = (AudioClip)Resources.Load(folder + "Heavy Forest", typeof(AudioClip));

            public static AudioClip WalkCaveLight = (AudioClip)Resources.Load(folder + "Light Cave", typeof(AudioClip));
            public static AudioClip WalkCaveHeavy = (AudioClip)Resources.Load(folder + "Heavy Cave", typeof(AudioClip));
        }

        public class Sword
        {
            private static string folder = Audio.folder + "Sword/";
            public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Swing1", typeof(AudioClip));
            public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Swing2", typeof(AudioClip));
            public static AudioClip HitFlesh = (AudioClip)Resources.Load(folder + "Hit Flesh", typeof(AudioClip));
            public static AudioClip HitObject = (AudioClip)Resources.Load(folder + "Hit Object", typeof(AudioClip));
        }

        public class Bow
        {
            private static string folder = Audio.folder + "Bow/";
            public static AudioClip ShootArrow = (AudioClip)Resources.Load(folder + "Shoot Arrow", typeof(AudioClip));
        }

        public class Arrow
        {
            private static string folder = Audio.folder + "Arrow/";
            public static AudioClip Hit = (AudioClip)Resources.Load(folder + "Arrow Hit", typeof(AudioClip));
            public static AudioClip Fly = (AudioClip)Resources.Load(folder + "Arrow Fly", typeof(AudioClip));
        }

        public class SpellSources
        {
            private static string folder = Audio.folder + "Spell Sources/";
            public static AudioClip Fire = (AudioClip)Resources.Load(folder + "Fire", typeof(AudioClip));
            public static AudioClip Ice = (AudioClip)Resources.Load(folder + "Ice", typeof(AudioClip));
            public static AudioClip Earth = (AudioClip)Resources.Load(folder + "Earth", typeof(AudioClip));
            public static AudioClip Lightning = (AudioClip)Resources.Load(folder + "Lightning", typeof(AudioClip));
            public static AudioClip Energy = (AudioClip)Resources.Load(folder + "Energy", typeof(AudioClip));
        }

        public class Spells
        {
            private static string folder = Audio.folder + "Spells/";
            public static AudioClip FireWall = (AudioClip)Resources.Load(folder + "Fire Wall", typeof(AudioClip));
        }
    }
}

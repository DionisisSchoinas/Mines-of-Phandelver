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
            public static AudioClip ButtonPick = (AudioClip)Resources.Load(folder + "Button Pick", typeof(AudioClip));
        }

        public class SkillIcons
        {
            private static string folder = UI.folder + "Skill Icons/";

            public class Dodge
            {
                private static string folder = SkillIcons.folder + "Dodge/";

                public static Sprite Roll = (Sprite)Resources.Load(folder + "Dash", typeof(Sprite));
                public static Sprite Dash = (Sprite)Resources.Load(folder + "Dash", typeof(Sprite));
            }

            public class Bolt
            {
                private static string folder = SkillIcons.folder + "Bolt/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Cone
            {
                private static string folder = SkillIcons.folder + "Cone/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Explosion
            {
                private static string folder = SkillIcons.folder + "Explosion/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Ray
            {
                private static string folder = SkillIcons.folder + "Ray/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Resistance
            {
                private static string folder = SkillIcons.folder + "Resistance/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Storm
            {
                private static string folder = SkillIcons.folder + "Storm/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Swing
            {
                private static string folder = SkillIcons.folder + "Swing/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Wall
            {
                private static string folder = SkillIcons.folder + "Wall/";

                public static Sprite Fire = (Sprite)Resources.Load(folder + "Fire", typeof(Sprite));
                public static Sprite Ice = (Sprite)Resources.Load(folder + "Ice", typeof(Sprite));
                public static Sprite Earth = (Sprite)Resources.Load(folder + "Earth", typeof(Sprite));
                public static Sprite Lightning = (Sprite)Resources.Load(folder + "Lightning", typeof(Sprite));
            }

            public class Default
            {
                private static string folder = SkillIcons.folder + "Default/";

                public static Sprite Homing = (Sprite)Resources.Load(folder + "Homing", typeof(Sprite));
                public static Sprite Swing = (Sprite)Resources.Load(folder + "Swing", typeof(Sprite));
            }
        }
    }

    public class Audio
    {
        private static string folder = "Audio/";

        public static AudioClip CannonFire = (AudioClip)Resources.Load(folder + "Cannon Fire", typeof(AudioClip));
        public static AudioClip BridgeBreak = (AudioClip)Resources.Load(folder + "Bridge Break", typeof(AudioClip));

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

            public static AudioClip HitFlesh = (AudioClip)Resources.Load(folder + "Hit Flesh", typeof(AudioClip));
            public static AudioClip HitObject = (AudioClip)Resources.Load(folder + "Hit Object", typeof(AudioClip));

            public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Swing1", typeof(AudioClip));
            public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Swing2", typeof(AudioClip));

            public static AudioClip[] Swings = new AudioClip[] { Swing1, Swing2 };
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

            public class Earth
            {
                private static string folder = Spells.folder + "Earth/";

                public static AudioClip BallTrail = (AudioClip)Resources.Load(folder + "Earth Ball Trail", typeof(AudioClip));

                public static AudioClip BigExplosion = (AudioClip)Resources.Load(folder + "Earth Big Explosion", typeof(AudioClip));
                public static AudioClip SmallExplosion = (AudioClip)Resources.Load(folder + "Earth Small Explosion", typeof(AudioClip));

                public static AudioClip Ray = (AudioClip)Resources.Load(folder + "Earth Ray", typeof(AudioClip));
                public static AudioClip Storm = (AudioClip)Resources.Load(folder + "Earth Storm", typeof(AudioClip));
                public static AudioClip Wall = (AudioClip)Resources.Load(folder + "Earth Wall", typeof(AudioClip));

                public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Earth Woosh 1", typeof(AudioClip));
                public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Earth Woosh 2", typeof(AudioClip));

                public static AudioClip[] Swings = new AudioClip[] { Swing1, Swing2 };
            }

            public class Energy
            {
                private static string folder = Spells.folder + "Energy/";

                public static AudioClip Impact1 = (AudioClip)Resources.Load(folder + "Impact 1", typeof(AudioClip));
                public static AudioClip Impact2 = (AudioClip)Resources.Load(folder + "Impact 2", typeof(AudioClip));
                public static AudioClip Impact3 = (AudioClip)Resources.Load(folder + "Impact 3", typeof(AudioClip));
                public static AudioClip Impact4 = (AudioClip)Resources.Load(folder + "Impact 4", typeof(AudioClip));

                public static AudioClip[] Impacts = new AudioClip[] { Impact1, Impact2, Impact3, Impact4 };
            }

            public class Fire
            {
                private static string folder = Spells.folder + "Fire/";

                public static AudioClip BigExplosion = (AudioClip)Resources.Load(folder + "Fire Big Explosion", typeof(AudioClip));
                public static AudioClip SmallExplosion = (AudioClip)Resources.Load(folder + "Fire Small Explosion", typeof(AudioClip));

                public static AudioClip Ray = (AudioClip)Resources.Load(folder + "Fire Ray", typeof(AudioClip));
                public static AudioClip Storm = (AudioClip)Resources.Load(folder + "Fire Storm", typeof(AudioClip));
                public static AudioClip Wall = (AudioClip)Resources.Load(folder + "Fire Wall", typeof(AudioClip));

                public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Fire Woosh 1", typeof(AudioClip));
                public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Fire Woosh 2", typeof(AudioClip));
                public static AudioClip Swing3 = (AudioClip)Resources.Load(folder + "Fire Woosh 3", typeof(AudioClip));

                public static AudioClip[] Swings = new AudioClip[] { Swing1, Swing2, Swing3 };
            }

            public class Ice
            {
                private static string folder = Spells.folder + "Ice/";

                public static AudioClip BigExplosion = (AudioClip)Resources.Load(folder + "Ice Big Explosion", typeof(AudioClip));
                public static AudioClip SmallExplosion = (AudioClip)Resources.Load(folder + "Ice Small Explosion", typeof(AudioClip));

                public static AudioClip Ray = (AudioClip)Resources.Load(folder + "Ice Ray", typeof(AudioClip));
                public static AudioClip Storm = (AudioClip)Resources.Load(folder + "Ice Storm", typeof(AudioClip));
                public static AudioClip Wall = (AudioClip)Resources.Load(folder + "Ice Wall", typeof(AudioClip));

                public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Ice Woosh 1", typeof(AudioClip));
                public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Ice Woosh 2", typeof(AudioClip));
                public static AudioClip Swing3 = (AudioClip)Resources.Load(folder + "Ice Woosh 3", typeof(AudioClip));

                public static AudioClip[] Swings = new AudioClip[] { Swing1, Swing2, Swing3 };
            }

            public class Lightning
            {
                private static string folder = Spells.folder + "Lightning/";

                public static AudioClip BigExplosion = (AudioClip)Resources.Load(folder + "Lightning Big Explosion", typeof(AudioClip));
                public static AudioClip SmallExplosion = (AudioClip)Resources.Load(folder + "Lightning Small Explosion", typeof(AudioClip));

                public static AudioClip Ray = (AudioClip)Resources.Load(folder + "Lightning Ray", typeof(AudioClip));
                public static AudioClip Storm = (AudioClip)Resources.Load(folder + "Lightning Storm", typeof(AudioClip));
                public static AudioClip Wall = (AudioClip)Resources.Load(folder + "Lightning Wall", typeof(AudioClip));

                public static AudioClip Swing1 = (AudioClip)Resources.Load(folder + "Lightning Woosh 1", typeof(AudioClip));
                public static AudioClip Swing2 = (AudioClip)Resources.Load(folder + "Lightning Woosh 2", typeof(AudioClip));

                public static AudioClip[] Swings = new AudioClip[] { Swing1, Swing2 };

                public static AudioClip Spark = (AudioClip)Resources.Load(folder + "Lightning Spark", typeof(AudioClip));
                public static AudioClip Arc = (AudioClip)Resources.Load(folder + "Lightning Arc", typeof(AudioClip));
            }
        }
    }
}

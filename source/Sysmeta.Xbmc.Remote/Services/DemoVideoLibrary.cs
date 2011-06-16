namespace Sysmeta.Xbmc.Remote.Services
{
    using System;

    using Sysmeta.Xbmc.Remote.Model;

    public class DemoVideoLibrary : IVideoLibrary
    {
        public void GetTvshows(Action<TvshowResult, Exception> callback)
        {
            var tvShows = new TvshowResult()
                {
                    Tvshows =
                        new[]
                            {
                                new Tvshow
                                    {
                                        Id = 1,
                                        Title = "Bones",
                                        Episode = 97,
                                        Plot = "A forensic anthropologist and a cocky FBI agent build a team to investigate death causes. And quite often, there isn't more to examine than rotten flesh or mere bones.",
                                        Genre = "Drama",
                                        Label = "Bones",
                                        PlayCount = 0,
                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/c/c9e0aea1.tbn"),
                                        Premiered = new DateTime(2005, 09, 13),
                                        Rating = 8.600000381469727f,
                                        Studio = "FOX",
                                        Year = 2005,
                                        Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c9e0aea1.tbn"),
                                    },
                                new Tvshow
                                    {
                                        Id = 2,
                                        Title = "Chuck",
                                        Episode = 62,
                                        Plot = "Meet Chuck Bartowski. He's your run of the mill computer whiz working for Buy-More with his equally nerdy friends all while longing for the woman of his dreams. But Chuck's average life is suddenly turned upside down when an old friend, who happens to work for the CIA, sends Chuck a mysterious e-mail and the world's closely-kept spy secrets are embedded in Chuck's brain. Now Chuck has unwittingly become the government's most powerful weapon and the fate of both the country and the world now lie in his hands. A Buy More day job and saving the world are all in a day's work for Chuck.\r\nChuck is put under the protection of two rival intelligence operatives, Sarah Walker and Colonel John Casey. They're forced to work together and keep Chuck alive while taking him on assignments where his unique knowledge can prove useful on missions. Sarah has to stay close to Chuck by pretending to be his girlfriend. Meanwhile, Chuck has to keep his secrets from best friend and co-worker Morgan Grimes, and his concerned sister Ellie Bartowski.",
                                        Genre = "Action and Adventure / Comedy / Drama",
                                        Label = "Chuck",
                                        PlayCount = 0,
                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/d/d968bc59.tbn"),
                                        Premiered = new DateTime(2007, 09, 24),
                                        Rating = 8.800000190734863f,
                                        Studio = "NBC",
                                        Year = 2007,
                                        Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/d968bc59.tbn"),
                                    }
                            }
                };

            callback(tvShows, null);
        }

        public void GetTvEpisodes(int tvshowId, int season, Action<TvEpisodesResult, Exception> callback)
        {
            if (tvshowId == 1 && season == 1)
            {
                var episodes = new TvEpisodesResult
                    #region Bones
                    {
                        Episodes =
                            new[]
                                {
                                    new TvEpisode
                                        {
                                            Id = 33,
                                            Title = "Pilot",
                                            Year = 2005,
                                            Rating = 7.1f,
                                            Director = "Greg Yaitanes",
                                            Plot =
                                                "Pilot episode. After returning to Washington, D.C., renowned forensic anthropologist Dr. Temperance Brennan is called in by FBI Special Agent Seeley Booth to aid an FBI investigation involving a set of bones found hidden in a lake.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-09-13",
                                            Duration = 2538,
                                            Season = 1,
                                            Episode = 1,
                                            PlayCount = 0,
                                            Writer = "Hart Hanson",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/2c1d245e.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 34,
                                            Title = "The Man in the S.U.V.",
                                            Year = 2005,
                                            Rating = 7.5f,
                                            Director = "Allan Kroeker",
                                            Plot =
                                                "When a man is killed driving an SUV registered to a Middle Eastern man, Dr. Brennan and her team find themselves tracking a terrorist.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-09-20",
                                            Duration = 2534,
                                            Season = 1,
                                            Episode = 2,
                                            PlayCount = 0,
                                            Writer = "Stephen Nathan",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/325a745b.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 35,
                                            Title = "A Boy in a Tree",
                                            Year = 2005,
                                            Rating = 7.7f,
                                            Director = "Patrick Norris",
                                            Plot =
                                                "When the decaying corpse of the Venezuelan Ambassador's son is found hanging from a tree on the campus of an exclusive private school, a simple suicide case quickly turns into a web of lies, sex and scandal involving the school's staff and students.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-09-27",
                                            Duration = 2517,
                                            Season = 1,
                                            Episode = 3,
                                            PlayCount = 0,
                                            Writer = "Hart Hanson",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/369b69ec.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 36,
                                            Title = "The Man in the Bear",
                                            Year = 2005,
                                            Rating = 7.5f,
                                            Director = "Allan Kroeker",
                                            Plot =
                                                "Brennan and Booth travel to Washington to investigate a human arm that was found in the stomach of a black bear.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-11-01",
                                            Duration = 2500,
                                            Season = 1,
                                            Episode = 4,
                                            PlayCount = 0,
                                            Writer = "Laura Wolner",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/3bd84f35.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 37,
                                            Title = "A Boy in a Bush",
                                            Year = 2005,
                                            Rating = 7.6f,
                                            Director = "Jesus Salvador Trevino",
                                            Plot =
                                                "Booth asks Brennan to help locate and identify the remains of a six-year-old boy, Charlie, who went missing from a local park.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-11-08",
                                            Duration = 2547,
                                            Season = 1,
                                            Episode = 5,
                                            PlayCount = 0,
                                            Writer = "Steve Blackman / Greg Ball",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/3f195282.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 38,
                                            Title = "The Man in the Wall",
                                            Year = 2005,
                                            Rating = 7.2f,
                                            Director = "Tawnia McKiernan",
                                            Plot =
                                                "When Brennan and Angela get caught in a fight in a dance club, Brennan kicks someone into a wall, which causes the wall to break open and reveal a mummified corpse and a meth stash.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-11-15",
                                            Duration = 2511,
                                            Season = 1,
                                            Episode = 6,
                                            PlayCount = 0,
                                            Writer = "Elizabeth Benjamin",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/0756ef3f.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 39,
                                            Title = "The Man on Death Row",
                                            Year = 2005,
                                            Rating = 7.9f,
                                            Director = "David Jones (III)",
                                            Plot =
                                                "A death row inmate asks for Booth's help to prove his innocence before he is scheduled to be executed. Booth agrees and asks Brennan and her team to help him out.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-11-22",
                                            Duration = 2543,
                                            Season = 1,
                                            Episode = 7,
                                            PlayCount = 0,
                                            Writer = "Noah Hawley",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/0397f288.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 40,
                                            Title = "The Girl in the Fridge",
                                            Year = 2005,
                                            Rating = 8.1f,
                                            Director = "Sanford Bookstaver",
                                            Plot =
                                                "Brennan is about to go out for dinner with her old professor and ex-lover when Booth asks her and her team to help identify the remains of a young woman found in a refrigerator.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-11-29",
                                            Duration = 2545,
                                            Season = 1,
                                            Episode = 8,
                                            PlayCount = 0,
                                            Writer = "Dana Coen",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/16917cfd.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 41,
                                            Title = "The Man in the Fallout Shelter",
                                            Year = 2005,
                                            Rating = 8f,
                                            Director = "Greg Yaitanes",
                                            Plot =
                                                "Brennan and her team are brought in to identify the body of a man found in a fallout shelter. While cutting into the bones of the man, Zach accidentally  releases a deadly fungus, causing the team to be quarantined over Christmas.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2005-12-13",
                                            Duration = 2543,
                                            Season = 1,
                                            Episode = 9,
                                            PlayCount = 0,
                                            Writer = "Hart Hanson",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/1250614a.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 42,
                                            Title = "The Woman at the Airport",
                                            Year = 2006,
                                            Rating = 7.3f,
                                            Director = "Greg Yaitanes",
                                            Plot =
                                                "Brennan and Booth investigate the body parts of a woman found near L.A. International Airport.  Their investigation is slowed due to extensive cosmetic surgery the victim had done.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-01-25",
                                            Duration = 2533,
                                            Season = 1,
                                            Episode = 10,
                                            PlayCount = 0,
                                            Writer = "Teresa Lin",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/1f134793.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 43,
                                            Title = "The Woman in the Car",
                                            Year = 2006,
                                            Rating = 7.4f,
                                            Director = "Dwight Little",
                                            Plot =
                                                "When a woman�s burned body is found in a car with signs that her child was kidnapped, Brennan and Booth suspect the father, Carl Decker. But things get complicated when Decker turns out to be in the Witness Protection program.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-02-01",
                                            Duration = 2539,
                                            Season = 1,
                                            Episode = 11,
                                            PlayCount = 0,
                                            Writer = "Noah Hawley",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/1bd25a24.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 44,
                                            Title = "The Superhero in the Alley",
                                            Year = 2006,
                                            Rating = 7.1f,
                                            Director = "James Whitmore Jr.",
                                            Plot =
                                                "Brennan and Booth investigate the origins of a decomposed body that's been found in a local alley and determine it's the remains of a shy teen with an alter ego.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-02-08",
                                            Duration = 2424,
                                            Season = 1,
                                            Episode = 12,
                                            PlayCount = 0,
                                            Writer = "Elizabeth Benjamin",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/05950a21.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 45,
                                            Title = "The Woman in the Garden",
                                            Year = 2006,
                                            Rating = 7.3f,
                                            Director = "Sanford Bookstaver",
                                            Plot =
                                                "Booth and Brennan investigate when a dug up corpse is found in the back of a gang member's car.  While being questioned, a drive-by shooting allows him to escape.  Booth and Brennan follow the clues, which lead them to an empty grave and another body in a grave at a wealthy senator's house, where the gang member used to work.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-02-15",
                                            Duration = 2506,
                                            Season = 1,
                                            Episode = 13,
                                            PlayCount = 0,
                                            Writer = "Laura Wolner",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/01541796.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 46,
                                            Title = "The Man on the Fairway",
                                            Year = 2006,
                                            Rating = 7.2f,
                                            Director = "Tony Wharmby",
                                            Plot =
                                                "Brennan and Zach investigate a small jet crash that was carrying some Chinese diplomats and a woman.  At the crash site, Brennan finds some bone fragments that do not belong to any of the passengers, but might belong to a man who has been missing for five years.  Although ordered to work only on the diplomats, Brennan and her team go rogue to try to discover the identity of the extra bones.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-03-08",
                                            Duration = 2533,
                                            Season = 1,
                                            Episode = 14,
                                            PlayCount = 0,
                                            Writer = "Steve Blackman",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/0c17314f.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 47,
                                            Title = "Two Bodies in the Lab",
                                            Year = 2006,
                                            Rating = 8f,
                                            Director = "Allan Kroeker",
                                            Plot =
                                                "When Brennan decides to give online dating a try, she ends up the target of a shooting at the local restaurant where she's meeting her date. The resulting investigation reveals that the shooter could be connected to either of the two cases she's currently working on.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-03-15",
                                            Duration = 2541,
                                            Season = 1,
                                            Episode = 15,
                                            PlayCount = 0,
                                            Writer = "Stephen Nathan",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/08d62cf8.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 48,
                                            Title = "The Woman in the Tunnel",
                                            Year = 2006,
                                            Rating = 7.3f,
                                            Director = "Joe Napolitano",
                                            Plot =
                                                "Brennan and Booth stumble upon a subterranean world of homeless people when they find the remains of a filmmaker in the ventilation shaft of an underground tunnel.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-03-22",
                                            Duration = 2541,
                                            Season = 1,
                                            Episode = 16,
                                            PlayCount = 0,
                                            Writer = "Greg Ball / Steve Blackman",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/30999145.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 49,
                                            Title = "The Skull in the Desert",
                                            Year = 2006,
                                            Rating = 7.4f,
                                            Director = "Donna Deitch",
                                            Plot =
                                                "While on vacation, Angela's boyfriend goes missing . When a skull shows up, she asks Brennan for help in identification, concerned it might be her boyfriend or the female guide who went missing with him. The investigation leads the team to a violent boyfriend, the local sheriff and ultimately to a counterfeiting ring setup in the desert.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-03-29",
                                            Duration = 2540,
                                            Season = 1,
                                            Episode = 17,
                                            PlayCount = 0,
                                            Writer = "Jeff Rake",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/34588cf2.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 50,
                                            Title = "The Man with the Bone",
                                            Year = 2006,
                                            Rating = 7.6f,
                                            Director = "Jesus Trevino",
                                            Plot =
                                                "A human finger bone found in the clutches of a drowning victim leads to a search for buried treasure.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-04-05",
                                            Duration = 2494,
                                            Season = 1,
                                            Episode = 18,
                                            PlayCount = 0,
                                            Writer = "Craig Silverstein",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c488bd21.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 51,
                                            Title = "The Man in the Morgue",
                                            Year = 2006,
                                            Rating = 7.8f,
                                            Director = "James Whitmore Jr.",
                                            Plot =
                                                "A trip to New Orleans leaves Bones beaten, bloodied, and suspected of murder while plunged into the world of voodoo.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-04-19",
                                            Duration = 2497,
                                            Season = 1,
                                            Episode = 19,
                                            PlayCount = 0,
                                            Writer = "Noah Hawley / Elizabeth Benjamin",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c049a096.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 52,
                                            Title = "The Graft in the Girl",
                                            Year = 2006,
                                            Rating = 7.6f,
                                            Director = "Sanford Bookstaver",
                                            Plot =
                                                "The team helps Deputy Director Sam Cullen's daughter, Amy, when she contracts a rare form of lung cancer after receiving a bone graft.  Booth and Brennan learn she is not the only victim with this rare form of cancer.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-04-26",
                                            Duration = 2503,
                                            Season = 1,
                                            Episode = 20,
                                            PlayCount = 0,
                                            Writer = "Laura Wolner / Greg Ball",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/cd0a864f.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 53,
                                            Title = "The Soldier on the Grave",
                                            Year = 2006,
                                            Rating = 7.5f,
                                            Director = "Jonathan Pontell",
                                            Plot =
                                                "Brennan and Booth investigate an apparent suicide in Arlington National Cemetery, but soon discover it is a murder linked to a military cover-up.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-05-10",
                                            Duration = 2542,
                                            Season = 1,
                                            Episode = 21,
                                            PlayCount = 0,
                                            Writer = "Stephen Nathan",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c9cb9bf8.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 54,
                                            Title = "The Woman in Limbo",
                                            Year = 2006,
                                            Rating = 8.1f,
                                            Director = "Jesus Trevino",
                                            Plot =
                                                "Brennan receives some bones to identify, but is stunned when she discovers that the remains are those of her mother who disappeared when she was 15 years old. The determined date of her mother's death leaves Bones confused and angry.",
                                            ShowTitle = "Bones",
                                            FirstAired = "2006-05-17",
                                            Duration = 2508,
                                            Season = 1,
                                            Episode = 22,
                                            PlayCount = 0,
                                            Writer = "Hart Hanson",
                                            Studio = "FOX",
                                            MPAA = "TV-14",
                                            Premiered = "2005-09-13",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/d78ccbfd.tbn")
                                        },
                                }
                    };
                #endregion

                callback(episodes, null);
            }
            else
            {
                var episodes = new TvEpisodesResult
                    #region Chuck

                    {
                        Episodes =
                            new[]
                                {
                                    new TvEpisode
                                        {
                                            Id = 99,
                                            Title = "Chuck Versus the Intersect",
                                            Year = 2007,
                                            Rating = 7.7f,
                                            Director = "McG",
                                            Plot =
                                                "Chuck Bartowski is an average computer geek until files of government secrets are downloaded into his brain. He is soon scouted by the CIA and NSA to act in place of their computer.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-09-24",
                                            Duration = 2601,
                                            Season = 1,
                                            Episode = 1,
                                            PlayCount = 0,
                                            Writer = "Josh Schwartz / Chris Fedak",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/0455385d.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 100,
                                            Title = "Chuck Versus the Wookiee",
                                            Year = 2007,
                                            Rating = 7.7f,
                                            Director = "Allan Kroeker",
                                            Plot =
                                                "Chuck's latest mission has him raiding a Malibu mansion to grab a million dollar diamond being used to fund terrorism. Tagging along is an attractive but less-than-trustworthy DEA agent.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-10-15",
                                            Duration = 2543,
                                            Season = 1,
                                            Episode = 4,
                                            PlayCount = 0,
                                            Writer = "Allison Adler",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c49aa0fa.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 101,
                                            Title = "Chuck Versus the Sizzling Shrimp",
                                            Year = 2007,
                                            Rating = 7.2f,
                                            Director = "David Solomon",
                                            Plot =
                                                "After his intuition is wrong while working on a stakeout, Chuck feels morally obligated to make things right, even when both Sarah and Casey refuse to help with Chuck's new mission. Meanwhile, Morgan has to participate in a sales competition that could decide his fate at Buy More.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-10-22",
                                            Duration = 2486,
                                            Season = 1,
                                            Episode = 5,
                                            PlayCount = 0,
                                            Writer = "Scott Rosenbaum",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c05bbd4d.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 102,
                                            Title = "Chuck Versus the Sandworm",
                                            Year = 2007,
                                            Rating = 7.6f,
                                            Director = "Robert Duncan McNeill",
                                            Plot =
                                                "In this Halloween episode Chuck meets a Federal agent just like him: A computer whiz whose brain is important to national security. However, Chuck soon has a personal dilemma arise when he has to decide whether or not to turn the man over to Sarah and Casey or to help him escape his handlers.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-10-29",
                                            Duration = 2432,
                                            Season = 1,
                                            Episode = 6,
                                            PlayCount = 0,
                                            Writer = "Phil Klemmer",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/cd189b94.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 103,
                                            Title = "Chuck Versus the Alma Mater",
                                            Year = 2007,
                                            Rating = 8f,
                                            Director = "Patrick Norris",
                                            Plot =
                                                "Returning to Stanford, the University that expelled him, Chuck has to help a former professor who is a government agent being hunted for a sensitive top-secret file he has in his possession. At the same time, Chuck learns much about himself and about the death of his friend Bryce Larkin.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-11-05",
                                            Duration = 2486,
                                            Season = 1,
                                            Episode = 7,
                                            PlayCount = 0,
                                            Writer = "Anne Cofell Saunders",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c9d98623.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 104,
                                            Title = "Chuck Versus the Truth",
                                            Year = 2007,
                                            Rating = 8.1f,
                                            Director = "Robert Duncan McNeill",
                                            Plot =
                                                "Chuck must learn to balance a real love life with his cover love life when he meets a charming young woman by the name of Lou. At the same time, his relationship with Sarah gets interesting when the duo must explain their sexual relations with Ellie and Captain Awesome on a double date. Meanwhile, there is trouble when someone is using truth poison in order to retrieve codes for nuclear facilities.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-11-12",
                                            Duration = 2542,
                                            Season = 1,
                                            Episode = 8,
                                            PlayCount = 0,
                                            Writer = "Allison Adler",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/d79ed626.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 105,
                                            Title = "Chuck Versus the Imported Hard Salami",
                                            Year = 2007,
                                            Rating = 8.1f,
                                            Director = "Jason Ensler",
                                            Plot =
                                                "Not all of Sarah's feelings for Chuck may have been a cover, as she finds out, when Chuck begins to date Lou, a sandwich maker. When things couldn't get even more complicated, Lou turns out to be part of a smuggling ring.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-11-19",
                                            Duration = 2542,
                                            Season = 1,
                                            Episode = 9,
                                            PlayCount = 0,
                                            Writer = "Matt Miller / Scott Rosenbaum",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/d35fcb91.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 106,
                                            Title = "Chuck Versus the Nemesis",
                                            Year = 2007,
                                            Rating = 8.1f,
                                            Director = "Allison Liddi-Brown",
                                            Plot =
                                                "Chuck discovers that his former roommate and old nemesis, Bryce Larkin, is still alive, and that a renegade group within the CIA is after the Intersect. Chuck then has to balance helping Bryce return to the legitimate CIA, with the year's busiest shopping day at the Buy More.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-11-26",
                                            Duration = 2515,
                                            Season = 1,
                                            Episode = 10,
                                            PlayCount = 0,
                                            Writer = "Chris Fedak",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/de1ced48.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 107,
                                            Title = "Chuck Versus the Crown Vic",
                                            Year = 2007,
                                            Rating = 7.5f,
                                            Director = "Chris Fisher",
                                            Plot =
                                                "While coping with the realization that Sarah still has feelings for Bryce, Chuck must assume his toughest cover yet by going on a mission as Sarah's husband. The team must foil the plot of a politically connected counterfeiter, who may be endangering the lives of Anna, Morgan and Anna's parents. Both Morgan and Anna also find themselves in the cross hairs.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2007-12-03",
                                            Duration = 2509,
                                            Season = 1,
                                            Episode = 11,
                                            PlayCount = 0,
                                            Writer = "Zev Borow",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/daddf0ff.tbn")
                                        },
                                    new TvEpisode
                                        {
                                            Id = 108,
                                            Title = "Chuck Versus the Undercover Lover",
                                            Year = 2008,
                                            Rating = 7.6f,
                                            Director = "Frederick E.O. Toye",
                                            Plot =
                                                "The episode begins with a flashback to the year 2004, where John Casey is involved with a female Russian named Ilsa, who claims she is a photo journalist. Later, in present day, Ilsa returns with a fianc�, named Victor Federov. Things get complicated when it is revealed that Ilsa is a French spy.",
                                            ShowTitle = "Chuck",
                                            FirstAired = "2008-01-24",
                                            Duration = 2548,
                                            Season = 1,
                                            Episode = 12,
                                            PlayCount = 0,
                                            Writer = "Phil Klemmer",
                                            Studio = "NBC",
                                            MPAA = "TV-PG",
                                            Premiered = "2007-09-24",
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/e2924d42.tbn")
                                        },
                                }
                    };

                #endregion

                callback(episodes, null);
            }
        }

        public void GetTvSeason(int tvshowId, Action<TvSeasonsResult, Exception> callback)
        {
            if (tvshowId == 1)
            {
                var seasons = new TvSeasonsResult
                    {
                        Seasons =
                            new[]
                                {
                                    new TvSeason
                                        {
                                            TvshowId = 1,
                                            Title = "Season 1",
                                            Episodes = 22,
                                            Season = 1,
                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/b/b8edf222.tbn"),
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/c9e0aea1.tbn")
                                        }
                                }
                    };

                callback(seasons, null);
            }
            else
            {
                var seasons = new TvSeasonsResult
                    {
                        Seasons =
                            new[]
                                {
                                    new TvSeason
                                        {
                                            TvshowId = 2,
                                            Title = "Season 1",
                                            Episodes = 10,
                                            Season = 1,
                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/0/01cd1dea.tbn"),
                                            Fanart = new Uri("special://masterprofile/Thumbnails/Video/Fanart/d968bc59.tbn")
                                        }
                                }
                    };

                callback(seasons, null);                
            }
        }

        public void GetMovies(Action<MoviesResult, Exception> callback)
        {
            var result = new MoviesResult()
                {
                    Movies = new[]
                        {
                            #region Movies
                            new Movie
                                {
                                    Director = @"Terry Gilliam",
                                    Genre = @"Science Fiction / Thriller",
                                    Id = 1,
                                    MPAA = @"Rated R",
                                    OriginalTitle = @"12 Monkeys",
                                    PlayCount = 1,
                                    Plot = @"In the year 2035, convict James Cole volunteers reluctantly to be sent back in time by scientists to discover the origin of a deadly virus that wiped out nearly all of the earth's population decades earlier. But when Cole is sent mistakenly to 1990 instead of 1996, he's arrested and locked up in a mental hospital, where he meets a psychiatrist and the son of a famous virus expert.",
                                    Rating = 8.2f,
                                    Runtime = @"130",
                                    Studio = @"Universal Studios",
                                    Tagline = @"The future is history",
                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/f/f7fc8691.tbn"),
                                    Title = @"12 Monkeys",
                                    Writer = @"David Webb Peoples / Janet Peoples",
                                    Year = 1995
                                },new Movie
                                    {
                                        Director = @"Doug Liman",
                                        Genre = @"Action / Drama / Mystery / Thriller",
                                        Id = 4,
                                        MPAA = @"Rated PG-13",
                                        OriginalTitle = @"The Bourne Identity",
                                        PlayCount = 0,
                                        Plot = @"Wounded to the brink of death and suffering from amnesia, Jason Bourne is rescued at sea by a fisherman. With nothing to go on but a Swiss bank account number, he starts to reconstruct his life, but finds that many people he encounters want him dead. Bourne realizes, though, that he has the combat and mental skills of a world-class spy, but who does he work for?",
                                        Rating = 8.6f,
                                        Runtime = @"119",
                                        Studio = @"Universal Pictures",
                                        Tagline = @"He was the perfect weapon until he became the target.",
                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/e/e5af7158.tbn"),
                                        Title = @"The Bourne Identity",
                                        Writer = @"Tony Gilroy / W. Blake Herron",
                                        Year = 2002
                                    },new Movie
                                        {
                                            Director = @"George P. Cosmatos",
                                            Genre = @"Action / Adventure / Drama / Thriller / War",
                                            Id = 15,
                                            MPAA = @"Rated R",
                                            OriginalTitle = @"Rambo: First Blood Part II",
                                            PlayCount = 0,
                                            Plot = @"Col. Troutman recruits ex-Green Beret John Rambo for a highly secret and dangerous mission. Teamed with freedom fighter Co Bao, Rambo goes deep into Vietnam to rescue POWs. Deserted by his own team, he's left in a hostile jungle to fight for his life, avenge the death of a woman and bring corrupt officials to justice.",
                                            Rating = 6f,
                                            Runtime = @"94",
                                            Studio = @"Anabasis N.V.",
                                            Tagline = @"What most people call hell, he calls home.",
                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/8/8917327c.tbn"),
                                            Title = @"Rambo: First Blood Part II",
                                            Writer = @"Sylvester Stallone / James Cameron",
                                            Year = 1985
                                        },new Movie
                                            {
                                                Director = @"Stuart Baird",
                                                Genre = @"Action / Adventure / Science Fiction / Thriller",
                                                Id = 19,
                                                MPAA = @"Rated PG-13",
                                                OriginalTitle = @"Star Trek: Nemesis",
                                                PlayCount = 0,
                                                Plot = @"Break out your Vulcan ears: The crew of the USS Enterprise is back. This time around, the Next Generation crew members -- including Capt. Picard, Cmdr. Riker and, of course, Data -- are dispatched as peace ambassadors to their longtime foes, the Romulans. There, they encounter a mysterious new enemy that thwarts not only the armistice process, but Earth itself!",
                                                Rating = 6.6f,
                                                Runtime = @"117",
                                                Studio = @"Paramount Pictures",
                                                Tagline = @"A generation's final journey... begins.",
                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/f/f84ca5a8.tbn"),
                                                Title = @"Star Trek: Nemesis",
                                                Writer = @"John Logan",
                                                Year = 2002
                                            },new Movie
                                                {
                                                    Director = @"John Glen",
                                                    Genre = @"Action / Adventure / Drama / Horror / Thriller",
                                                    Id = 23,
                                                    MPAA = @"Rated PG",
                                                    OriginalTitle = @"A View to a Kill",
                                                    PlayCount = 0,
                                                    Plot = @"A newly developed microchip designed by Zorin Industries for the British Government that can survive the electromagnetic radiation caused by a nuclear explosion has landed in the hands of the KGB. James Bond must find out how and why. His suspicions soon lead him to big industry leader Max Zorin. The 14th film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                                    Rating = 6.4f,
                                                    Runtime = @"131",
                                                    Studio = @"Danjag S.A. / EON Productions / United Artists / Metro-Goldwyn-Mayer",
                                                    Tagline = @"Has James Bond finally met his match?",
                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/2/2efe0c22.tbn"),
                                                    Title = @"A View to a Kill",
                                                    Writer = @"Richard Maibaum / Michael J. Wilson",
                                                    Year = 1985
                                                },new Movie
                                                    {
                                                        Director = @"Val Guest / Ken Hughes / John Huston / Joseph McGrath / Robert Parrish",
                                                        Genre = @"Action / Adventure / Comedy",
                                                        Id = 24,
                                                        MPAA = null,
                                                        OriginalTitle = @"Casino Royale",
                                                        PlayCount = 0,
                                                        Plot = @"Sir James Bond is called back out of retirement to stop SMERSH. In order to trick SMERSH, Bond thinks up the ultimate plan. That every agent will be named James Bond. One of the Bonds, whose real name is Evelyn Tremble is sent to take on Le Chiffre in a game of baccarat, but all the Bonds get more than they can handle, especially when the ultimate villain turns out to be Bonds nephew.",
                                                        Rating = 8.6f,
                                                        Runtime = @"131",
                                                        Studio = null,
                                                        Tagline = null,
                                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/3/3145107b.tbn"),
                                                        Title = @"Casino Royale",
                                                        Writer = @"Wolf Mankowitz / John Law / Michael Sayers",
                                                        Year = 1967
                                                    },new Movie
                                                        {
                                                            Director = @"Guy Hamilton",
                                                            Genre = @"Action / Adventure / Thriller",
                                                            Id = 25,
                                                            MPAA = @"Rated PG",
                                                            OriginalTitle = @"Diamonds Are Forever",
                                                            PlayCount = 0,
                                                            Plot = @"Diamonds are stolen only to be sold again in the international market. James Bond infiltrates a smuggling mission to find out who�s guilty. The mission takes him to Las Vegas where Bong meets his archenemy Blofeld. The seventh film from the Legendary James Bond series starring Sean Connery as a British super agent.",
                                                            Rating = 6.6f,
                                                            Runtime = @"120",
                                                            Studio = @"Danjag S.A. / EON Productions",
                                                            Tagline = @"Bond is back...with the girls",
                                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/8/8ff6746d.tbn"),
                                                            Title = @"Diamonds Are Forever",
                                                            Writer = @"Richard Maibaum / Tom Mankiewicz",
                                                            Year = 1971
                                                        },new Movie
                                                            {
                                                                Director = @"Lee Tamahori",
                                                                Genre = @"Action / Adventure / Thriller",
                                                                Id = 26,
                                                                MPAA = @"Rated PG-13",
                                                                OriginalTitle = @"Die Another Day",
                                                                PlayCount = 0,
                                                                Plot = @"The ever-daring James Bond taking on a North Korean leader who undergoes DNA replacement procedures that allow him to assume different identities. American agent Jinx Johnson assists Bond in thwarting the villain's plans to exploit a satellite that is powered by solar energy.",
                                                                Rating = 7f,
                                                                Runtime = @"133",
                                                                Studio = @"EON Productions / Danjag S.A. / Metro-Goldwyn-Mayer (MGM) / United Artists",
                                                                Tagline = @"Events don't get any bigger than...",
                                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/2/2f478cc7.tbn"),
                                                                Title = @"Die Another Day",
                                                                Writer = null,
                                                                Year = 2002
                                                            },new Movie
                                                                {
                                                                    Director = @"Terence Young",
                                                                    Genre = @"Action / Thriller",
                                                                    Id = 27,
                                                                    MPAA = @"Rated PG",
                                                                    OriginalTitle = @"Dr. No",
                                                                    PlayCount = 0,
                                                                    Plot = @"The mysterious scientist Dr. No sabotages the American space program from his secret base in Jamaica. The Secret Service sends it�s best agent after this him. Dr. No is the first film of legendary James Bond series starring Sean Connery in the role of a British super agent. ",
                                                                    Rating = 7.4f,
                                                                    Runtime = @"111",
                                                                    Studio = @"EON Productions",
                                                                    Tagline = @"NOW meet the most extraordinary gentleman spy in all fiction!",
                                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/d/d1ae99e8.tbn"),
                                                                    Title = @"Dr. No",
                                                                    Writer = @"Richard Maibaum / Johanna Harwood / Berkely Mather",
                                                                    Year = 1962
                                                                },new Movie
                                                                    {
                                                                        Director = @"John Glen",
                                                                        Genre = @"Action / Adventure / Thriller",
                                                                        Id = 28,
                                                                        MPAA = @"Rated PG",
                                                                        OriginalTitle = @"For Your Eyes Only",
                                                                        PlayCount = 0,
                                                                        Plot = @"A British spy ship has sunk and on board was a hi-tech encryption device. James Bond is sent to find the device that holds British launching instructions before the enemy Soviets get to it first. The twelfth film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                                                        Rating = 7.8f,
                                                                        Runtime = @"127",
                                                                        Studio = @"Danjag S.A. / EON Productions / United Artists",
                                                                        Tagline = @"No one comes close to James Bond, 007.",
                                                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/4/4a5ddf2a.tbn"),
                                                                        Title = @"For Your Eyes Only",
                                                                        Writer = @"Richard Maibaum / Michael J. Wilson",
                                                                        Year = 1981
                                                                    },new Movie
                                                                        {
                                                                            Director = @"Terence Young",
                                                                            Genre = @"Action / Thriller",
                                                                            Id = 29,
                                                                            MPAA = @"Rated PG",
                                                                            OriginalTitle = @"From Russia with Love",
                                                                            PlayCount = 0,
                                                                            Plot = @"Bond is back and on the loose in exotic Istanbul looking for a super-secret coding machine. He's involved with a beautiful Russian spy and has the SPECTRE organization after him, including villainess Rosa Klebb (she of the killer shoe). Lots of exciting escapes but not an overreliance on the gadgetry of the later films. The second Bond feature, thought by many to be the best.",
                                                                            Rating = 8.6f,
                                                                            Runtime = @"110",
                                                                            Studio = @"EON Productions",
                                                                            Tagline = @"The world's masters of murder pull out all the stops to destroy Agent 007!",
                                                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/0/001d4636.tbn"),
                                                                            Title = @"From Russia with Love",
                                                                            Writer = @"Richard Maibaum",
                                                                            Year = 1963
                                                                        },new Movie
                                                                            {
                                                                                Director = @"Martin Campbell",
                                                                                Genre = @"Action / Adventure / Thriller",
                                                                                Id = 30,
                                                                                MPAA = @"Rated PG-13",
                                                                                OriginalTitle = @"GoldenEye",
                                                                                PlayCount = 0,
                                                                                Plot = @"James Bond must unmask the mysterious head of the Janus Syndicate and prevent his one time ally - Alec Trevelyan Agent 006 - from utilising the GoldenEye weapons system to inflict devastating revenge on Britain. --- The 17th film from the legendary James Bond series and the first starring Pierce Brosnan.",
                                                                                Rating = 7.6f,
                                                                                Runtime = @"130",
                                                                                Studio = @"Danjag S.A. / EON Productions / Metro-Goldwyn-Mayer / United Artists",
                                                                                Tagline = @"When the world is the target and the threat is real, you can still depend on one man.",
                                                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/5/534b7569.tbn"),
                                                                                Title = @"GoldenEye",
                                                                                Writer = @"Jeffrey Caine / Bruce Feirstein",
                                                                                Year = 1995
                                                                            },new Movie
                                                                                {
                                                                                    Director = @"Guy Hamilton",
                                                                                    Genre = @"Action / Thriller",
                                                                                    Id = 31,
                                                                                    MPAA = @"Rated PG",
                                                                                    OriginalTitle = @"Goldfinger",
                                                                                    PlayCount = 0,
                                                                                    Plot = @"Someone is planning a major robbery of Gold that could destroy the economy. James Bond is dispatched by the MI6 and the Bank of England to find the people responsible and stop them. This is the third film from the  legendary James Bond series starring Sean Connery as the British super agent.",
                                                                                    Rating = 9.2f,
                                                                                    Runtime = @"112",
                                                                                    Studio = @"EON Productions",
                                                                                    Tagline = @"Everything he touches turns into excitement!",
                                                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/1/107a3d25.tbn"),
                                                                                    Title = @"Goldfinger",
                                                                                    Writer = @"Richard Maibaum / Paul Dehn",
                                                                                    Year = 1964
                                                                                },new Movie
                                                                                    {
                                                                                        Director = @"John Glen",
                                                                                        Genre = @"Action / Thriller",
                                                                                        Id = 32,
                                                                                        MPAA = @"Rated PG-13",
                                                                                        OriginalTitle = @"Licence to Kill",
                                                                                        PlayCount = 0,
                                                                                        Plot = @"James Bond and his American colleague Felix Leiter arrest the drug lord Sanchez who succeeds in escaping and takes revenge on Felix and his wife. Bond knows but just one thing: revenge. The 16th film from the Legendary James Bond series starring Timothy Dalton as a British super agent.",
                                                                                        Rating = 6.4f,
                                                                                        Runtime = @"133",
                                                                                        Studio = @"Danjag S.A. / EON Productions / United Artists",
                                                                                        Tagline = @"His bad side is a dangerous place to be.",
                                                                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/0/0d5ee9b4.tbn"),
                                                                                        Title = @"Licence to Kill",
                                                                                        Writer = @"Michael J. Wilson / Richard Maibaum",
                                                                                        Year = 1989
                                                                                    },new Movie
                                                                                        {
                                                                                            Director = @"Guy Hamilton",
                                                                                            Genre = @"Action / Thriller",
                                                                                            Id = 33,
                                                                                            MPAA = @"Rated PG",
                                                                                            OriginalTitle = @"Live and Let Die",
                                                                                            PlayCount = 0,
                                                                                            Plot = @"James Bond must investigate a mysterious murder case of a British agent in New York. Soon he finds himself up against a big gangster boss named Mr. Big. This the eight film of the James Bond series and the first one to star Roger Moore as the British undercover agent 007.",
                                                                                            Rating = 7.6f,
                                                                                            Runtime = @"121",
                                                                                            Studio = @"Danjag S.A. / EON Productions",
                                                                                            Tagline = @"Roger M007re is James Bond.",
                                                                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/6/63c997e0.tbn"),
                                                                                            Title = @"Live and Let Die",
                                                                                            Writer = @"Tom Mankiewicz",
                                                                                            Year = 1973
                                                                                        },new Movie
                                                                                            {
                                                                                                Director = @"Lewis Gilbert",
                                                                                                Genre = @"Action / Adventure / Thriller",
                                                                                                Id = 34,
                                                                                                MPAA = @"Rated PG",
                                                                                                OriginalTitle = @"Moonraker",
                                                                                                PlayCount = 0,
                                                                                                Plot = @"During the transportation of a Space Shuttle a Boeing 747 crashes in the Atlantic Ocean yet when they go to look for the destroyed shuttle it is not there. James bond investigates the missing mission space shuttle and soon learns that the shuttles owner Hugo Drax wants to kill all of mankind. The showdown is about to take place in outer space. The eleventh film from the Legendary James Bond series",
                                                                                                Rating = 7.2f,
                                                                                                Runtime = @"126",
                                                                                                Studio = @"Danjag S.A. / EON Productions / Les Productions Artistes Associ�s",
                                                                                                Tagline = @"Outer space now belongs to 007.",
                                                                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/b/b42d93a1.tbn"),
                                                                                                Title = @"Moonraker",
                                                                                                Writer = @"Christopher Wood",
                                                                                                Year = 1979
                                                                                            },new Movie
                                                                                                {
                                                                                                    Director = @"Irvin Kershner",
                                                                                                    Genre = @"Action / Adventure / Thriller",
                                                                                                    Id = 35,
                                                                                                    MPAA = @"Rated PG",
                                                                                                    OriginalTitle = @"Never Say Never Again",
                                                                                                    PlayCount = 0,
                                                                                                    Plot = @"James Bond (Sean Connery) returns as the secret agent 007 one more time to battle the evil organization SPECTRE. Bond must defeat Largo,who has stolen two atomic warheads for nuclear blackmail. But Bond has an ally in largo's girlfriend, the willowy Domino (Kim Basinger),who falls for Bond and seeks revenge. This is the last time for Sean Connery as Her Majesty's Secret Agent 007.",
                                                                                                    Rating = 5.6f,
                                                                                                    Runtime = @"133",
                                                                                                    Studio = @"PSO International / TaliaFilm II Productions / Warner Bros. Pictures / Woodcote",
                                                                                                    Tagline = @"Sean Connery is James Bond 007",
                                                                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/1/1242c0a8.tbn"),
                                                                                                    Title = @"Never Say Never Again",
                                                                                                    Writer = null,
                                                                                                    Year = 1983
                                                                                                },new Movie
                                                                                                    {
                                                                                                        Director = @"John Glen",
                                                                                                        Genre = @"Action / Adventure / Thriller",
                                                                                                        Id = 36,
                                                                                                        MPAA = @"Rated PG",
                                                                                                        OriginalTitle = @"Octopussy",
                                                                                                        PlayCount = 0,
                                                                                                        Plot = @"James Bond is sent to investigate after a fellow �00� agent is found dead with a priceless Farberge egg. James bond follows the mystery and uncovers a smuggling scandal and a Russian General who wants to provoke a new World War. The 13th film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                                                                                        Rating = 7.6f,
                                                                                                        Runtime = @"131",
                                                                                                        Studio = @"EON Productions / Danjag S.A. / United Artists / Metro-Goldwyn-Mayer",
                                                                                                        Tagline = @"Nobody does him better.",
                                                                                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/6/65e5c555.tbn"),
                                                                                                        Title = @"Octopussy",
                                                                                                        Writer = @"George MacDonald Fraser / Richard Maibaum / Michael J. Wilson",
                                                                                                        Year = 1983
                                                                                                    },new Movie
                                                                                                        {
                                                                                                            Director = @"Peter R. Hunt",
                                                                                                            Genre = @"Action / Thriller",
                                                                                                            Id = 37,
                                                                                                            MPAA = @"Rated PG",
                                                                                                            OriginalTitle = @"On Her Majesty's Secret Service",
                                                                                                            PlayCount = 0,
                                                                                                            Plot = @"Bond tracks archnemesis Ernst Blofeld to a mountaintop retreat where he's training an army of beautiful but lethal women. Along the way, Bond falls for Italian contessa Tracy Draco -- and marries her in order to get closer to Blofeld. Meanwhile, he locates Blofeld in the Alps and embarks on a classic ski chase.",
                                                                                                            Rating = 8f,
                                                                                                            Runtime = @"140",
                                                                                                            Studio = @"EON Productions / Danjag S.A.",
                                                                                                            Tagline = @"Far up! Far out! Far more! James Bond 007 is back!",
                                                                                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/7/7ad68a24.tbn"),
                                                                                                            Title = @"On Her Majesty's Secret Service",
                                                                                                            Writer = @"Richard Maibaum",
                                                                                                            Year = 1969
                                                                                                        },new Movie
                                                                                                            {
                                                                                                                Director = @"Marc Forster",
                                                                                                                Genre = @"Action / Adventure / Thriller",
                                                                                                                Id = 38,
                                                                                                                MPAA = @"Rated PG-13",
                                                                                                                OriginalTitle = @"Quantum of Solace",
                                                                                                                PlayCount = 0,
                                                                                                                Plot = @"Quantum of Solace continues the high octane adventures of James Bond in Casino Royale. Betrayed by Vesper, the woman he loved, 007 fights the urge to make his latest mission personal. Pursuing his determination to uncover the truth, Bond and M interrogate Mr White who reveals the organisation which blackmailed Vesper is far more complex and dangerous than anyone had imagined.",
                                                                                                                Rating = 7f,
                                                                                                                Runtime = @"106",
                                                                                                                Studio = @"Metro-Goldwyn-Mayer",
                                                                                                                Tagline = @"For love, for hate, for justice, for revenge.",
                                                                                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/4/40424448.tbn"),
                                                                                                                Title = @"Quantum of Solace",
                                                                                                                Writer = @"Robert Wade / Neal Purvis / Paul Haggis",
                                                                                                                Year = 2008
                                                                                                            },new Movie
                                                                                                                {
                                                                                                                    Director = @"John Glen",
                                                                                                                    Genre = @"Action / Adventure / Thriller",
                                                                                                                    Id = 39,
                                                                                                                    MPAA = @"Rated PG",
                                                                                                                    OriginalTitle = @"The Living Daylights",
                                                                                                                    PlayCount = 0,
                                                                                                                    Plot = @"James Bond helps a Russian General escape into the west. He soon finds out that the KGB wants to kill him for helping the General. A little while later the General is kidnapped from the Secret Service leading 007 to be suspicious. The 15th film from the Legendary James Bond series starring Timothy Dalton as a British super agent.",
                                                                                                                    Rating = 7.2f,
                                                                                                                    Runtime = @"130",
                                                                                                                    Studio = @"Danjag S.A. / EON Productions / United Artists",
                                                                                                                    Tagline = @"Licensed to thrill.",
                                                                                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/6/6157c47a.tbn"),
                                                                                                                    Title = @"The Living Daylights",
                                                                                                                    Writer = @"Richard Maibaum / Michael J. Wilson",
                                                                                                                    Year = 1987
                                                                                                                },new Movie
                                                                                                                    {
                                                                                                                        Director = @"Guy Hamilton",
                                                                                                                        Genre = @"Action / Adventure / Thriller",
                                                                                                                        Id = 40,
                                                                                                                        MPAA = @"Rated PG",
                                                                                                                        OriginalTitle = @"The Man with the Golden Gun",
                                                                                                                        PlayCount = 0,
                                                                                                                        Plot = @"A golden bullet has 007 engraved on it as it smashes into the secret service headquarters. The bullet came from the professional killer Scaramanga who has yet to miss a target and James Bond begins a mission to try and stop him. The ninth film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                                                                                                        Rating = 7f,
                                                                                                                        Runtime = @"125",
                                                                                                                        Studio = @"Danjag S.A. / EON Productions",
                                                                                                                        Tagline = @"The man with the golden gun is ready to assassinate James Bond.",
                                                                                                                        Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/f/ff2b872c.tbn"),
                                                                                                                        Title = @"The Man with the Golden Gun",
                                                                                                                        Writer = @"Richard Maibaum / Tom Mankiewicz",
                                                                                                                        Year = 1974
                                                                                                                    },new Movie
                                                                                                                        {
                                                                                                                            Director = @"Lewis Gilbert",
                                                                                                                            Genre = @"Action / Adventure / Thriller",
                                                                                                                            Id = 41,
                                                                                                                            MPAA = @"Rated PG",
                                                                                                                            OriginalTitle = @"The Spy Who Loved Me",
                                                                                                                            PlayCount = 0,
                                                                                                                            Plot = @"Russian and American submarines with nuclear missels on board both vanish from sight without a trace. America and Russia both blame each other as James Bond tries to solve the riddle of the disappearing ships. But the KGB also has an agent on the case. The tenth film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                                                                                                            Rating = 8f,
                                                                                                                            Runtime = @"125",
                                                                                                                            Studio = @"EON Productions / Danjag S.A.",
                                                                                                                            Tagline = @"It's the BIGGEST. It's the BEST. It's BOND. And B-E-Y-O-N-D.",
                                                                                                                            Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/5/5363d34a.tbn"),
                                                                                                                            Title = @"The Spy Who Loved Me",
                                                                                                                            Writer = @"Christopher Wood / Richard Maibaum",
                                                                                                                            Year = 1977
                                                                                                                        },new Movie
                                                                                                                            {
                                                                                                                                Director = @"Michael Apted",
                                                                                                                                Genre = @"Action / Adventure / Thriller",
                                                                                                                                Id = 42,
                                                                                                                                MPAA = @"Rated PG-13",
                                                                                                                                OriginalTitle = @"The World Is Not Enough",
                                                                                                                                PlayCount = 0,
                                                                                                                                Plot = @"Greed, revenge, world dominance, high-tech terrorism -- it's all in a day's work for cunning MI6 agent James Bond, who's on a mission to protect beautiful oil heiress Elektra King from a notorious terrorist. In a race against time that culminates in a dramatic submarine showdown, Bond works to defuse the international power struggle that has the world's oil supply hanging in the balance.",
                                                                                                                                Rating = 8.4f,
                                                                                                                                Runtime = @"128",
                                                                                                                                Studio = @"Danjaq / EON Productions / Metro-Goldwyn-Mayer (MGM) / United Artists",
                                                                                                                                Tagline = @"As the countdown begins for the new millienum there is still one number you can always count on.",
                                                                                                                                Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/f/f81c8006.tbn"),
                                                                                                                                Title = @"The World Is Not Enough",
                                                                                                                                Writer = null,
                                                                                                                                Year = 1999
                                                                                                                            },new Movie
                                                                                                                                {
                                                                                                                                    Director = @"Terence Young",
                                                                                                                                    Genre = @"Action / Thriller",
                                                                                                                                    Id = 43,
                                                                                                                                    MPAA = @"Rated PG",
                                                                                                                                    OriginalTitle = @"Thunderball",
                                                                                                                                    PlayCount = 0,
                                                                                                                                    Plot = @"A criminal organization has obtained two nuclear bombs and are asking for a 100 million pound ransom in the form of diamonds in seven days or they will use the weapons. The secret service sends James Bond to the Bahamas to once again save the world.",
                                                                                                                                    Rating = 7.4f,
                                                                                                                                    Runtime = @"130",
                                                                                                                                    Studio = @"EON Productions",
                                                                                                                                    Tagline = @"Look up!  Look down!  Look out!",
                                                                                                                                    Thumbnail = new Uri("special://masterprofile/Thumbnails/Video/2/2cc36602.tbn"),
                                                                                                                                    Title = @"Thunderball",
                                                                                                                                    Writer = @"Jack Whittingham / Richard Maibaum / John Hopkins",
                                                                                                                                    Year = 1965
                                                                                                                                },
                            #endregion
                        }
                };

            callback(result, null);
        }

        public void GetRecentlyAddedMovies(Action<MoviesResult, Exception> callback)
        {
            var result = new MoviesResult
                {
                    Movies = new[]
                        {
                            #region Movies
                            new Movie
                                {
                                    Director = @"Terry Gilliam",
                                    Genre = @"Science Fiction / Thriller",
                                    Id = 1,
                                    MPAA = @"Rated R",
                                    OriginalTitle = @"12 Monkeys",
                                    PlayCount = 1,
                                    Plot =
                                        @"In the year 2035, convict James Cole volunteers reluctantly to be sent back in time by scientists to discover the origin of a deadly virus that wiped out nearly all of the earth's population decades earlier. But when Cole is sent mistakenly to 1990 instead of 1996, he's arrested and locked up in a mental hospital, where he meets a psychiatrist and the son of a famous virus expert.",
                                    Rating = 8.2f,
                                    Runtime = @"130",
                                    Studio = @"Universal Studios",
                                    Tagline = @"The future is history",
                                    Thumbnail =
                                        new Uri("special://masterprofile/Thumbnails/Video/f/f7fc8691.tbn"),
                                    Title = @"12 Monkeys",
                                    Writer = @"David Webb Peoples / Janet Peoples",
                                    Year = 1995
                                },
                            new Movie
                                {
                                    Director = @"Doug Liman",
                                    Genre = @"Action / Drama / Mystery / Thriller",
                                    Id = 4,
                                    MPAA = @"Rated PG-13",
                                    OriginalTitle = @"The Bourne Identity",
                                    PlayCount = 0,
                                    Plot =
                                        @"Wounded to the brink of death and suffering from amnesia, Jason Bourne is rescued at sea by a fisherman. With nothing to go on but a Swiss bank account number, he starts to reconstruct his life, but finds that many people he encounters want him dead. Bourne realizes, though, that he has the combat and mental skills of a world-class spy, but who does he work for?",
                                    Rating = 8.6f,
                                    Runtime = @"119",
                                    Studio = @"Universal Pictures",
                                    Tagline = @"He was the perfect weapon until he became the target.",
                                    Thumbnail =
                                        new Uri("special://masterprofile/Thumbnails/Video/e/e5af7158.tbn"),
                                    Title = @"The Bourne Identity",
                                    Writer = @"Tony Gilroy / W. Blake Herron",
                                    Year = 2002
                                },
                            new Movie
                                {
                                    Director = @"George P. Cosmatos",
                                    Genre = @"Action / Adventure / Drama / Thriller / War",
                                    Id = 15,
                                    MPAA = @"Rated R",
                                    OriginalTitle = @"Rambo: First Blood Part II",
                                    PlayCount = 0,
                                    Plot =
                                        @"Col. Troutman recruits ex-Green Beret John Rambo for a highly secret and dangerous mission. Teamed with freedom fighter Co Bao, Rambo goes deep into Vietnam to rescue POWs. Deserted by his own team, he's left in a hostile jungle to fight for his life, avenge the death of a woman and bring corrupt officials to justice.",
                                    Rating = 6f,
                                    Runtime = @"94",
                                    Studio = @"Anabasis N.V.",
                                    Tagline = @"What most people call hell, he calls home.",
                                    Thumbnail =
                                        new Uri("special://masterprofile/Thumbnails/Video/8/8917327c.tbn"),
                                    Title = @"Rambo: First Blood Part II",
                                    Writer = @"Sylvester Stallone / James Cameron",
                                    Year = 1985
                                },
                            new Movie
                                {
                                    Director = @"Stuart Baird",
                                    Genre = @"Action / Adventure / Science Fiction / Thriller",
                                    Id = 19,
                                    MPAA = @"Rated PG-13",
                                    OriginalTitle = @"Star Trek: Nemesis",
                                    PlayCount = 0,
                                    Plot =
                                        @"Break out your Vulcan ears: The crew of the USS Enterprise is back. This time around, the Next Generation crew members -- including Capt. Picard, Cmdr. Riker and, of course, Data -- are dispatched as peace ambassadors to their longtime foes, the Romulans. There, they encounter a mysterious new enemy that thwarts not only the armistice process, but Earth itself!",
                                    Rating = 6.6f,
                                    Runtime = @"117",
                                    Studio = @"Paramount Pictures",
                                    Tagline = @"A generation's final journey... begins.",
                                    Thumbnail =
                                        new Uri("special://masterprofile/Thumbnails/Video/f/f84ca5a8.tbn"),
                                    Title = @"Star Trek: Nemesis",
                                    Writer = @"John Logan",
                                    Year = 2002
                                },
                            new Movie
                                {
                                    Director = @"John Glen",
                                    Genre = @"Action / Adventure / Drama / Horror / Thriller",
                                    Id = 23,
                                    MPAA = @"Rated PG",
                                    OriginalTitle = @"A View to a Kill",
                                    PlayCount = 0,
                                    Plot =
                                        @"A newly developed microchip designed by Zorin Industries for the British Government that can survive the electromagnetic radiation caused by a nuclear explosion has landed in the hands of the KGB. James Bond must find out how and why. His suspicions soon lead him to big industry leader Max Zorin. The 14th film from the Legendary James Bond series starring Roger Moore as a British super agent.",
                                    Rating = 6.4f,
                                    Runtime = @"131",
                                    Studio =
                                        @"Danjag S.A. / EON Productions / United Artists / Metro-Goldwyn-Mayer",
                                    Tagline = @"Has James Bond finally met his match?",
                                    Thumbnail =
                                        new Uri("special://masterprofile/Thumbnails/Video/2/2efe0c22.tbn"),
                                    Title = @"A View to a Kill",
                                    Writer = @"Richard Maibaum / Michael J. Wilson",
                                    Year = 1985
                                },
                            new Movie
                                {
                                    Director =
                                        @"Val Guest / Ken Hughes / John Huston / Joseph McGrath / Robert Parrish",
                                    Genre = @"Action / Adventure / Comedy",
                                    Id = 24,
                                    MPAA = null,
                                    OriginalTitle = @"Casino Royale",
                                    PlayCount = 0,
                                    Plot =
                                        @"Sir James Bond is called back out of retirement to stop SMERSH. In order to trick SMERSH, Bond thinks up the ultimate plan. That every agent will be named James Bond. One of the Bonds, whose real name is Evelyn Tremble is sent to take on Le Chiffre in a game of baccarat, but all the Bonds get more than they can handle, especially when the ultimate villain turns out to be Bonds nephew.",
                                    Rating = 8.6f,
                                    Runtime = @"131",
                                    Studio = null,
                                    Tagline = null,
                                    Thumbnail =
                                        new Uri(
                                        "special://masterprofile/Thumbnails/Video/3/3145107b.tbn"),
                                    Title = @"Casino Royale",
                                    Writer = @"Wolf Mankowitz / John Law / Michael Sayers",
                                    Year = 1967
                                },

                            #endregion
                        }
                };

            callback(result, null);
        }
    }
}
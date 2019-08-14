// Denis super code 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LeaderBordView : MonoBehaviour, ILeaderboard {
    public bool loading {
        get
        {
            throw new NotImplementedException();
        }
    }
    public string id { get { throw new NotImplementedException(); }
        set
        {
            throw new NotImplementedException();
        }
    }
    public UserScope userScope { get { throw new NotImplementedException(); }
        set
        {
            throw new NotImplementedException();
        }
    }
    public Range range { get { throw new NotImplementedException(); }
        set
        {
            throw new NotImplementedException();
        }
    }
    public TimeScope timeScope { get { throw new NotImplementedException(); }
        set
        {
            throw new NotImplementedException();
        }
    }

    public IScore localUserScore {
        get
        {
            throw new NotImplementedException();
        }
    }

    public uint maxRange {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IScore[] scores {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string title {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void LoadScores(Action<bool> callback)
    {
        throw new NotImplementedException();
    }

    public void SetUserFilter(string[] userIDs)
    {
        throw new NotImplementedException();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}

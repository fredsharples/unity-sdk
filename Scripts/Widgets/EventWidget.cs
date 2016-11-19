﻿
/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Watson.DeveloperCloud.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IBM.Watson.DeveloperCloud.Widgets
{

  /// <summary>
  /// This Event Widget class maps events to a SerializedDelegate.
  /// </summary>
  public class EventWidget : Widget
  {
    #region Widget interface
    /// <exclude />
    protected override string GetName()
    {
      return "Event";
    }
    #endregion

    #region Private Data
    [Serializable]
    private class Mapping
    {
      public string _event = "";
      public SerializedDelegate callback = new SerializedDelegate(typeof(EventManager.OnReceiveEvent));
    };

    [SerializeField]
    private List<Mapping> mappings = new List<Mapping>();
    #endregion

    #region Event Handlers
    private void OnEnable()
    {
      foreach (var mapping in mappings)
      {
        EventManager.Instance.RegisterEventReceiver(mapping._event, mapping.callback.ResolveDelegate() as EventManager.OnReceiveEvent);
      }
    }

    private void OnDisable()
    {
      foreach (var mapping in mappings)
      {
        EventManager.Instance.UnregisterEventReceiver(mapping._event, mapping.callback.ResolveDelegate() as EventManager.OnReceiveEvent);
      }
    }
    #endregion
  }

}
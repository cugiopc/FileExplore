//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IController.cs" company="SMEE">
//    Copyright (c) SMEE, 2016
//    All rights are reserved. Reproduction or transmission in whole or in part, in
//    any form or by any means, electronic, mechanical or otherwise, is prohibited
//    without the prior written consent of the copyright owner.
//  </copyright>
//  <summary>
//    Defines the IController.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace FileExplorer.Contract.Controller
{
    using System.Collections.Generic;
    using FileExplorer.Contract;
    using Model;

    public interface IController<T>
    {
        IList<T> GetItems(FilterSetting filterSetting);

        IList<ResultInfo<T>> IncreaseVersion(IList<T> selectedItems);
    }
}
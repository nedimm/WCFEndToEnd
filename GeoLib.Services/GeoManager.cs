﻿using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
        IZipCodeRepository _zipCodeRepository = null;
        IStateRepository _stateRepository = null;

        public GeoManager()
        {
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _zipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _zipCodeRepository = zipCodeRepository;
            _stateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }
            return zipCodeData;
        }
        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            List<string> stateData = new List<string>();
            IStateRepository stateRepository = _stateRepository ?? new StateRepository();
            IEnumerable<State> states = stateRepository.Get(primaryOnly);
            if (states != null)
            {
                foreach (var state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }
            return stateData;
        }
        
        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

            var zips = zipCodeRepository.GetByState(state);
            if (zips != null)
            {
                foreach(ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);

            if (zips != null)
            {
                foreach (ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zipCodeData;
        }
    }
}

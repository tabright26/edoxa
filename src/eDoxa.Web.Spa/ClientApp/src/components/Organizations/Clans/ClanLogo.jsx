import React, { useEffect, useState, Fragment } from "react";

import { connectLogo } from "store/organizations/logos/container";

import ClanLogoForm from "forms/Organizations/ClanLogo";

const ClanLogo = ({ actions, clanId, logos, isOwner }) => {
  const [showLogo, setShowLogo] = useState(null);

  useEffect(() => {
    if (clanId) {
      actions.loadLogo(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    if (logos) {
      setShowLogo(logos.find(logo => logo.clanId === clanId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [logos, clanId]);

  return (
    <Fragment>
      <img src={showLogo ? showLogo : "https://via.placeholder.com/350x150"} alt="Team logo"></img>
      {isOwner ? <ClanLogoForm.Update onSubmit={data => actions.updateLogo(clanId, data)} /> : ""}
    </Fragment>
  );
};

export default connectLogo(ClanLogo);

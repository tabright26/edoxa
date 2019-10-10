import React, { Fragment } from "react";

import ClanLogoForm from "forms/Organizations/ClanLogo";

const imageStyle = {
  height: "100%",
  width: "100%",
  objectFit: "contain"
};

const ClanLogo = ({ actions, logo, clanId, isOwner }) => {
  return (
    <Fragment>
      <img style={imageStyle} src={logo ? logo : "https://via.placeholder.com/350x150"} alt="Team logo"></img>
      {isOwner ? <ClanLogoForm.Update onSubmit={data => actions.updateLogo(clanId, data)} /> : ""}
    </Fragment>
  );
};

export default ClanLogo;

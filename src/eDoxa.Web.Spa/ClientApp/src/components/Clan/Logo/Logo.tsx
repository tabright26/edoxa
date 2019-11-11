import React, { Fragment, FunctionComponent } from "react";

import ClanLogoForm from "forms/Organization/Clan/Logo";

const imageStyle = {
  height: "100%",
  width: "100%"
};

const ClanLogo: FunctionComponent<any> = ({ actions, logo, clanId, isOwner }) => {
  return (
    <Fragment>
      <img style={imageStyle} src={logo ? logo : "https://via.placeholder.com/350x150"} alt="Team logo"></img>
      {isOwner && <ClanLogoForm.Upload onSubmit={data => actions.updateLogo(clanId, data)} />}
    </Fragment>
  );
};

export default ClanLogo;

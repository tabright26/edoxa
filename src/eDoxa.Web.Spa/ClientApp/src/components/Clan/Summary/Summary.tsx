import React, { Fragment } from "react";

const ClanInfo = ({ clan }) => {
  return (
    <Fragment>
      Name: {clan.name}
      <br />
      ID: {clan.id}
      <br />
      Summary: {clan.summary}
      <br />
      Members: {clan.members.length}
      <br />
      {clan.ownerDoxatag}
      <br />
      OwnerId: {clan.ownerId}
    </Fragment>
  );
};

export default ClanInfo;

import React, { useState, useEffect } from "react";
import { Badge } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import CandidatureForm from "forms/Organizations/Candidatures";

const InvitationWidget = ({ actions, candidatures, clanId, userId }) => {
  useEffect(() => {
    actions.loadCandidaturesWithClanId(clanId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const candidatureExists = candidatures.find(candidature => candidature.userId === userId);

  return !candidatureExists ? (
    <CandidatureForm.Create initialValues={{ userId: userId, clanId: clanId }} onSubmit={data => actions.addCandidature(data)} />
  ) : (
    <Badge color="success">You already invited this member to your clan.</Badge>
  );
};

export default connectCandidatures(InvitationWidget);

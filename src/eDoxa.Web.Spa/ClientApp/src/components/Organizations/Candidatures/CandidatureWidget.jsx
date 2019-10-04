import React, { useState, useEffect } from "react";
import { Badge } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import CandidatureForm from "forms/Organizations/Candidatures";

//HOW TO GET USER ID MORE EASILY

const CandidatureWidget = ({ actions, candidatures, clanId, userId }) => {
  useEffect(() => {
    actions.loadCandidaturesWithClanId(clanId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [candidatures]);

  const candidatureExists = candidatures.find(candidature => candidature.userId === userId);

  return !candidatureExists ? (
    <CandidatureForm.Create initialValues={{ userId: userId, clanId: clanId }} onSubmit={data => actions.addCandidature(data)} />
  ) : (
    <Badge color="success">Candidature already sent.</Badge>
  );
};

export default connectCandidatures(CandidatureWidget);

import React, { useEffect } from "react";
import { Badge } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import CandidatureForm from "forms/Organizations/Candidatures";

const ClanCandidatureWidget = ({ actions, candidatures, clan, userId }) => {
  useEffect(() => {
    if (clan) {
      actions.loadCandidaturesWithClanId(clan.id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [candidatures, clan, userId]);

  const candidatureExists = candidatures.find(candidature => candidature.userId === userId);

  return !candidatureExists ? (
    <CandidatureForm.Create initialValues={{ userId: userId, clanId: clan.id }} onSubmit={data => actions.addCandidature(data)} />
  ) : (
    <Badge color="success">Candidature already sent.</Badge>
  );
};

export default connectCandidatures(ClanCandidatureWidget);

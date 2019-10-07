import React, { useEffect, useState } from "react";

import { connectCandidatures } from "store/organizations/candidatures/container";

import CandidatureForm from "forms/Organizations/Candidatures";

const CandidatureWidget = ({ actions, candidatures, clanId, userId }) => {
  const [sent, setSent] = useState(null);

  useEffect(() => {
    if (userId) {
      actions.loadCandidaturesWithUserId(userId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userId]);

  useEffect(() => {
    if (candidatures) {
      candidatures.forEach(candidature => {
        if (candidature.clanId === clanId) {
          setSent(true);
        }
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [candidatures]);

  const handleAddCandidature = data => {
    if (actions) {
      setSent(true);
      actions.addCandidature(data);
    }
  };

  return sent ? "Candidature already sent." : <CandidatureForm.Create initialValues={{ userId: userId, clanId: clanId }} onSubmit={data => handleAddCandidature(data)} />;
};

export default connectCandidatures(CandidatureWidget);

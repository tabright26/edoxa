import React, { useEffect, useState } from "react";
import { toastr } from "react-redux-toastr";
import { Button } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

//TODO. LE TRUC C<EST QUE ON FAIT UN RELOAD POUR CHAQUE WIDGE TDANS LA PAGE

const CandidatureWidget = ({ actions, candidatures, clanId, userId }) => {
  const [candidatureDisabled, setCandidatureDisabled] = useState(false);

  useEffect(() => {
    setCandidatureDisabled(candidatures.some(candidature => candidature.clanId === clanId));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Button color="primary" onClick={() => actions.addCandidature(clanId, userId).then(toastr.success("SUCCESS", "Candidature was sent successfully."))} disabled={candidatureDisabled}>
      Send candidature
    </Button>
  );
};
export default connectCandidatures(CandidatureWidget);

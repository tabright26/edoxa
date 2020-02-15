import React, { FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFacebook,
  faTwitter,
  faDiscord,
  faLinkedin,
  faInstagram
} from "@fortawesome/free-brands-svg-icons";
import {
  REACT_APP_FACEBOOK_URL,
  REACT_APP_TWITTER_URL,
  REACT_APP_DISCORD_URL,
  REACT_APP_LINKEDIN_URL,
  REACT_APP_INSTAGRAM_URL
} from "keys";

type Props = {
  className?: string;
};

export const Social: FunctionComponent<Props> = ({ className = null }) => (
  <ul className={`m-0 p-0 ${className}`}>
    <li className="d-inline mr-3">
      <a href={REACT_APP_FACEBOOK_URL}>
        <FontAwesomeIcon icon={faFacebook} size="lg" />
      </a>
    </li>
    <li className="d-inline mx-3">
      <a href={REACT_APP_TWITTER_URL}>
        <FontAwesomeIcon icon={faTwitter} size="lg" />
      </a>
    </li>
    <li className="d-inline mx-3">
      <a href={REACT_APP_DISCORD_URL}>
        <FontAwesomeIcon icon={faDiscord} size="lg" />
      </a>
    </li>
    <li className="d-inline mx-3">
      <a href={REACT_APP_LINKEDIN_URL}>
        <FontAwesomeIcon icon={faLinkedin} size="lg" />
      </a>
    </li>
    <li className="d-inline ml-3">
      <a href={REACT_APP_INSTAGRAM_URL}>
        <FontAwesomeIcon icon={faInstagram} size="lg" />
      </a>
    </li>
  </ul>
);

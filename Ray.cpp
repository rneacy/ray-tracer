#include "Ray.h"

Colour Ray::Colour_Lerp(const Ray& r, const Colour& gradA, const Colour& gradB) {
	Vector3 u_direction = Norm(r.Direction());
	auto t = .5 * (u_direction.y() + 1.0);
	return (1.0 - t) * gradA + t * gradB; // lerp on normed y
}

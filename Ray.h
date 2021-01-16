#pragma once

#include "Vector3.h"

class Ray {
public:
	Ray() {}
	Ray(const Point3& origin, const Vector3& direction) :
		_origin(origin), _direction(direction)
	{}

	Point3 Origin() const {
		return _origin;
	}

	Point3 Direction() const {
		return _direction;
	}

	// Gets the point at some stamp t along the ray.
	Point3 At(double t) const {
		return _origin + t * _direction;
	}

	static Colour Colour_Lerp(const Ray& r, const Colour& gradA, const Colour& gradB);

private:
	Point3 _origin;
	Vector3 _direction;
};